using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Suplex.Security.AclModel;
using Suplex.Security.DataAccess;
using Suplex.Security.Principal;
using Suplex.Utilities.Serialization;

using Newtonsoft.Json;

namespace Suplex.Security.WebApi
{
    class Program
    {
        static void Main(string[] args)
        {
            string pace = " {\r\n      \"UId\": \"5595682b-1045-4114-af8b-090307242578\",\r\n      \"RightType\": \"Suplex.Security.AclModel.FileSystemRight, Suplex.Security.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\r\n      \"Right\": \"TakeOwnership\",\r\n      \"Allowed\": true,\r\n      \"Inheritable\": true,\r\n      \"InheritedFrom\": \"9570128e-fba8-4455-b328-f30af56eabef\",\r\n      \"TrusteeUId\": \"d8adefb2-a142-4397-82b3-9b0d9df37d08\"\r\n    }";
            string aace = "{\r\n  \"UId\": \"3ac08eaa-700a-4ab4-9a90-1659db9ea25d\",\r\n  \"RightType\": \"Suplex.Security.AclModel.RecordRight, Suplex.Security.Core, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\",\r\n  \"Right\": \"List, Insert, Delete\",\r\n  \"Allowed\": true,\r\n  \"Denied\": false,\r\n  \"Inheritable\": true,\r\n  \"InheritedFrom\": \"9733efc2-1cde-415e-af79-ff2d74f5e69d\",\r\n  \"TrusteeUId\": \"d8adefb2-a142-4397-82b3-9b0d9df37d08\"\r\n}";
            JsonAceConverter aceConverter = new JsonAceConverter();
            IAccessControlEntry ace = JsonConvert.DeserializeObject<IAccessControlEntry>( aace, aceConverter );

            string json = JsonConvert.SerializeObject( ace, aceConverter );

            SuplexSecurityHttpApiClient client = new SuplexSecurityHttpApiClient( "http://localhost:20000/suplex/" );
            // test secure object
            SecureObject so = client.GetSecureObjectByUniqueName( "New Root1", includeChildren: false, includeDisabled: true );
            Console.WriteLine( $"Original Parent {so.ParentUId}" );
            SecureObject soDest = client.GetSecureObjectByUniqueName( "top.edited", includeChildren: false, includeDisabled: true );

            //client.UpdateSecureObjectParentUId( so, soDest.UId );
            //client.UpdateSecureObjectParentUId( so, null );
            //client.UpdateSecureObjectParentUId( so.UId, soDest.UId );
            client.UpdateSecureObjectParentUId( so.UId, null );
            SecureObject found = client.GetSecureObjectByUniqueName("New Root1", includeChildren: false, includeDisabled: true );
            Console.WriteLine( $"After update Parent {found.ParentUId}" );
            Console.WriteLine( "pause" );
        }
    }

    public class SuplexSecurityHttpApiClient : HttpApiClientBase, ISuplexDal
    {
        string _rootPath = "/suplex";
        bool _configureAwaitContinueOnCapturedContext = true;

        public SuplexSecurityHttpApiClient(string baseUrl, string messageFormatType = "application/json", bool configureAwaitContinueOnCapturedContext = true) : base( baseUrl, messageFormatType )
        {
            _configureAwaitContinueOnCapturedContext = configureAwaitContinueOnCapturedContext;
            Uri uri = new Uri( baseUrl );
            _rootPath = uri.AbsolutePath.TrimEnd( '/' );
        }


        public string Hello() { return HelloAsync().Result; }

        public async Task<string> HelloAsync()
        {
            string requestUri = $"{_rootPath}/hello";
            return await GetAsync<string>( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public string WhoAmI() { return WhoAmIAsync().Result; }

        public async Task<string> WhoAmIAsync()
        {
            string requestUri = $"{_rootPath}/hello/whoami";
            return await GetAsync<string>( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }



        public User GetUserByUId(Guid userUId)
        {
            return GetUserByUIdAsync( userUId ).Result;
        }

        public async Task<User> GetUserByUIdAsync(Guid userUId)
        {
            string requestUri = $"{_rootPath}/users/{userUId}";
            return await GetAsync<User>( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public List<User> GetUserByName(string name, bool exact = false)
        {
            return GetUserByNameAsync( name, exact ).Result;
        }

        public async Task<List<User>> GetUserByNameAsync(string name, bool exact = false)
        {
            string requestUri = $"{_rootPath}/users/?{nameof( name )}={name}&{nameof( exact )}={exact}";
            return await GetAsync<List<User>>( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public User UpsertUser(User user)
        {
            return UpsertUserAsync( user ).Result;
        }

        public async Task<User> UpsertUserAsync(User user)
        {
            string requestUri = $"users/";
            return await PostAsync<User>( user, requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public void DeleteUser(Guid userUId)
        {
            DeleteUserAsync( userUId ).Wait();
        }

        public async Task DeleteUserAsync(Guid userUId)
        {
            string requestUri = $"users/{userUId}/";
            await DeleteAsync( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }



        public Group GetGroupByUId(Guid groupUId)
        {
            return GetGroupByUIdAsync( groupUId ).Result;
        }

        public async Task<Group> GetGroupByUIdAsync(Guid groupUId)
        {
            string requestUri = $"{_rootPath}/groups/{groupUId}";
            return await GetAsync<Group>( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public List<Group> GetGroupByName(string name, bool exact = false)
        {
            return GetGroupByNameAsync( name, exact ).Result;
        }

        public async Task<List<Group>> GetGroupByNameAsync(string name, bool exact = false)
        {
            string requestUri = $"{_rootPath}/groups/?{nameof( name )}={name}&{nameof( exact )}={exact}";
            return await GetAsync<List<Group>>( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public Group UpsertGroup(Group group)
        {
            return UpsertGroupAsync( group ).Result;
        }

        public async Task<Group> UpsertGroupAsync(Group group)
        {
            string requestUri = $"groups/";
            return await PostAsync<Group>( group, requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public void DeleteGroup(Guid groupUId)
        {
            DeleteGroupAsync( groupUId ).Wait();
        }

        public async Task DeleteGroupAsync(Guid groupUId)
        {
            string requestUri = $"groups/{groupUId}/";
            await DeleteAsync( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }



        public IEnumerable<GroupMembershipItem> GetGroupMembership()
        {
            return GetGroupMembershipAsync().Result;
        }

        public async Task<IEnumerable<GroupMembershipItem>> GetGroupMembershipAsync()
        {
            string requestUri = $"{_rootPath}/gm/";
            return await GetAsync<IEnumerable<GroupMembershipItem>>( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public IEnumerable<GroupMembershipItem> GetGroupMembers(Guid groupUId, bool includeDisabledMembership = false)
        {
            return GetGroupMembersAsync( groupUId, includeDisabledMembership ).Result;
        }

        public async Task<IEnumerable<GroupMembershipItem>> GetGroupMembersAsync(Guid groupUId, bool includeDisabledMembership = false)
        {
            string requestUri = $"{_rootPath}/gm/{groupUId}/members/?{nameof( includeDisabledMembership )}={includeDisabledMembership}";
            return await GetAsync<IEnumerable<GroupMembershipItem>>( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public IEnumerable<GroupMembershipItem> GetGroupMemberOf(Guid memberUId, bool includeDisabledMembership = false)
        {
            return GetGroupMemberOfAsync( memberUId, includeDisabledMembership ).Result;
        }

        public async Task<IEnumerable<GroupMembershipItem>> GetGroupMemberOfAsync(Guid memberUId, bool includeDisabledMembership = false)
        {
            string requestUri = $"{_rootPath}/gm/{memberUId}/memberof/?{nameof( includeDisabledMembership )}={includeDisabledMembership}";
            return await GetAsync<IEnumerable<GroupMembershipItem>>( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public IEnumerable<GroupMembershipItem> GetGroupMembershipHierarchy(Guid memberUId, bool includeDisabledMembership = false)
        {
            return GetGroupMembershipHierarchyAsync( memberUId, includeDisabledMembership ).Result;
        }

        public async Task<IEnumerable<GroupMembershipItem>> GetGroupMembershipHierarchyAsync(Guid memberUId, bool includeDisabledMembership = false)
        {
            string requestUri = $"{_rootPath}/gm/{memberUId}/hier/?{nameof( includeDisabledMembership )}={includeDisabledMembership}";
            return await GetAsync<IEnumerable<GroupMembershipItem>>( requestUri, new JsonSecurityPrincipalBaseConverter() ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public GroupMembershipItem UpsertGroupMembership(GroupMembershipItem groupMembershipItem)
        {
            return UpsertGroupMembershipAsync( groupMembershipItem ).Result;
        }

        public async Task<GroupMembershipItem> UpsertGroupMembershipAsync(GroupMembershipItem groupMembershipItem)
        {
            string requestUri = $"gm/";
            return await PostAsync<GroupMembershipItem>( groupMembershipItem, requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public IEnumerable<GroupMembershipItem> UpsertGroupMembership(IEnumerable<GroupMembershipItem> groupMembershipItems)
        {
            return UpsertGroupMembershipAsync( groupMembershipItems ).Result;
        }

        public async Task<IEnumerable<GroupMembershipItem>> UpsertGroupMembershipAsync(IEnumerable<GroupMembershipItem> groupMembershipItems)
        {
            string requestUri = $"gm/items/";
            return await PostAsync<IEnumerable<GroupMembershipItem>>( groupMembershipItems, requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public void DeleteGroupMembership(GroupMembershipItem groupMembershipItem)
        {
            DeleteGroupMembershipAsync( groupMembershipItem ).Wait();
        }

        public async Task DeleteGroupMembershipAsync(GroupMembershipItem groupMembershipItem)
        {
            string requestUri = $"gm/{groupMembershipItem.GroupUId}/?memberUId={groupMembershipItem.MemberUId}";
            await DeleteAsync( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public void DeleteGroupMembership(IEnumerable<GroupMembershipItem> groupMembershipItems)
        {
            DeleteGroupMembershipAsync( groupMembershipItems ).Wait();
        }

        public async Task DeleteGroupMembershipAsync(IEnumerable<GroupMembershipItem> groupMembershipItems)
        {
            //string requestUri = $"gm/{groupMembershipItem.GroupUId}/?memberUId={groupMembershipItem.MemberUId}";
            string requestUri = $"gm/items";
            await DeleteAsync( groupMembershipItems, requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public MembershipList<SecurityPrincipalBase> GetGroupMembersList(Guid groupUId, bool includeDisabledMembership = false)
        {
            return GetGroupMembersListAsync( groupUId, includeDisabledMembership ).Result;
        }

        public async Task<MembershipList<SecurityPrincipalBase>> GetGroupMembersListAsync(Guid groupUId, bool includeDisabledMembership = false)
        {
            string requestUri = $"{_rootPath}/gm/ml/{groupUId}/members/?{nameof( includeDisabledMembership )}={includeDisabledMembership}";

            return await GetAsync<MembershipList<SecurityPrincipalBase>>( requestUri, new JsonSecurityPrincipalBaseConverter() ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public MembershipList<SecurityPrincipalBase> GetGroupMembersList(Group group, bool includeDisabledMembership = false)
        {
            return GetGroupMembersListAsync( group.UId, includeDisabledMembership ).Result;
        }

        public async Task<MembershipList<SecurityPrincipalBase>> GetGroupMembersListAsync(Group group, bool includeDisabledMembership = false)
        {
            return await GetGroupMembersListAsync( group.UId, includeDisabledMembership );
        }

        public MembershipList<Group> GetGroupMemberOfList(Guid memberUId, bool isMemberGroup = false, bool includeDisabledMembership = false)
        {
            return GetGroupMemberOfListAsync( memberUId, isMemberGroup, includeDisabledMembership ).Result;
        }

        public async Task<MembershipList<Group>> GetGroupMemberOfListAsync(Guid memberUId, bool isMemberGroup = false, bool includeDisabledMembership = false)
        {
            string requestUri = $"{_rootPath}/gm/ml/{memberUId}/memberof/?{nameof( isMemberGroup )}={isMemberGroup}&{nameof( includeDisabledMembership )}={includeDisabledMembership}";
            return await GetAsync<MembershipList<Group>>( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public MembershipList<Group> GetGroupMemberOfList(SecurityPrincipalBase member, bool includeDisabledMembership = false)
        {
            return GetGroupMemberOfListAsync( member.UId, member is Group, includeDisabledMembership ).Result;
        }

        public async Task<MembershipList<Group>> GetGroupMemberOfListAsync(SecurityPrincipalBase member, bool includeDisabledMembership = false)
        {
            return await GetGroupMemberOfListAsync( member.UId, member is Group, includeDisabledMembership );
        }



        public IEnumerable<SecureObject> GetSecureObjects()
        {
            return GetSecureObjectsAsync().Result;
        }

        IEnumerable<ISecureObject> ISuplexDal.GetSecureObjects()
        {
            return GetSecureObjectsAsync().Result;
        }

        public async Task<IEnumerable<SecureObject>> GetSecureObjectsAsync()
        {
            string requestUri = $"{_rootPath}/so/all/";
            return await GetAsync<IEnumerable<SecureObject>>( requestUri, new JsonAceConverter() ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public SecureObject GetSecureObjectByUId(Guid secureObjectUId, bool includeChildren, bool includeDisabled = false)
        {
            return GetSecureObjectByUIdAsync( secureObjectUId, includeChildren, includeDisabled ).Result;
        }

        ISecureObject ISuplexDal.GetSecureObjectByUId(Guid secureObjectUId, bool includeChildren, bool includeDisabled)
        {
            return GetSecureObjectByUIdAsync( secureObjectUId, includeChildren, includeDisabled ).Result;
        }

        public async Task<SecureObject> GetSecureObjectByUIdAsync(Guid secureObjectUId, bool includeChildren, bool includeDisabled = false)
        {
            string requestUri = $"{_rootPath}/so/{secureObjectUId}/?{nameof( includeChildren )}={includeChildren}&{nameof( includeDisabled )}={includeDisabled}";
            return await GetAsync<SecureObject>( requestUri, new JsonAceConverter() ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public SecureObject GetSecureObjectByUniqueName(string uniqueName, bool includeChildren, bool includeDisabled = false)
        {
            return GetSecureObjectByUniqueNameAsync( uniqueName, includeChildren, includeDisabled ).Result;
        }

        ISecureObject ISuplexDal.GetSecureObjectByUniqueName(string uniqueName, bool includeChildren, bool includeDisabled)
        {
            return GetSecureObjectByUniqueNameAsync( uniqueName, includeChildren, includeDisabled ).Result;
        }

        public async Task<SecureObject> GetSecureObjectByUniqueNameAsync(string uniqueName, bool includeChildren, bool includeDisabled = false)
        {
            string requestUri = $"{_rootPath}/so/?{nameof( uniqueName )}={uniqueName}&{nameof( includeChildren )}={includeChildren}&{nameof( includeDisabled )}={includeDisabled}";
            return await GetAsync<SecureObject>( requestUri, new JsonAceConverter() ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public SecureObject UpsertSecureObject(SecureObject secureObject)
        {
            return UpsertSecureObjectAsync( secureObject ).Result;
        }

        ISecureObject ISuplexDal.UpsertSecureObject(ISecureObject secureObject)
        {
            return UpsertSecureObjectAsync( secureObject as SecureObject ).Result;
        }

        public async Task<SecureObject> UpsertSecureObjectAsync(SecureObject secureObject)
        {
            string requestUri = $"{_rootPath}/so/";
            return await PostAsync<SecureObject>( secureObject, requestUri, new JsonAceConverter() ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public void UpdateSecureObjectParentUId(ISecureObject secureObject, Guid? newParentUId)
        {
            UpdateSecureObjectParentUIdAsync( secureObject, newParentUId ).Wait();
        }

        public async Task UpdateSecureObjectParentUIdAsync(ISecureObject secureObject, Guid? newParentUId)
        {
            
            string requestUri = $"{_rootPath}/so/{newParentUId}/";
            await PutAsync( secureObject, requestUri, new JsonAceConverter() ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public void UpdateSecureObjectParentUId(Guid secureObjectUId, Guid? newParentUId)
        {
            UpdateSecureObjectParentUIdAsync( secureObjectUId, newParentUId ).Wait();
        }

        public async Task UpdateSecureObjectParentUIdAsync(Guid secureObjectUId, Guid? newParentUId)
        {
            string requestUri = $"{_rootPath}/so/uid/{secureObjectUId}/{newParentUId}/";
            await PutAsync( null as Object, requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public void DeleteSecureObject(Guid secureObjectUId)
        {
            DeleteSecureObjectAsync( secureObjectUId ).Wait();
        }

        public async Task DeleteSecureObjectAsync(Guid secureObjectUId)
        {
            string requestUri = $"{_rootPath}/so/{secureObjectUId}/";
            await DeleteAsync( requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public ISecureObject EvalSecureObjectSecurity(string uniqueName, string userName, IEnumerable<string> externalGroupMembership = null)
        {
            return EvalSecureObjectSecurityAsync( uniqueName, userName, externalGroupMembership ).Result;
        }

        public async Task<ISecureObject> EvalSecureObjectSecurityAsync(string uniqueName, string userName, IEnumerable<string> externalGroupMembership = null)
        {
            string requestUri = $"{_rootPath}/so/so/eval/{uniqueName}/{userName}";
            return await PostAsync<IEnumerable<string>,ISecureObject>( externalGroupMembership, requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }

        public ISecureObject EvalSecureObjectSecurity(Guid secureObjectUId, Guid userUId, IEnumerable<string> externalGroupMembership = null)
        {
            return EvalSecureObjectSecurityAsync( secureObjectUId, userUId, externalGroupMembership ).Result;
        }

        public async Task<ISecureObject> EvalSecureObjectSecurityAsync(Guid secureObjectUId, Guid userUId, IEnumerable<string> externalGroupMembership = null)
        {
            string requestUri = $"{_rootPath}/so/so/eval/{secureObjectUId}/{secureObjectUId}";
            return await PostAsync<IEnumerable<string>, ISecureObject>( externalGroupMembership, requestUri ).ConfigureAwait( _configureAwaitContinueOnCapturedContext );
        }
    }
}