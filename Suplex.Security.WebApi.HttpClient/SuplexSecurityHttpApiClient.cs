using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Suplex.Security.AclModel;
using Suplex.Security.DataAccess;
using Suplex.Security.Principal;


namespace Suplex.Security.WebApi
{
    public class SuplexSecurityHttpApiClient : HttpApiClientBase, ISuplexDal
    {
        string _rootPath = "/synapse/execute";

        public SuplexSecurityHttpApiClient(string baseUrl, string messageFormatType = "application/json") : base( baseUrl, messageFormatType )
        {
        }


        public string Hello() { return HelloAsync().Result; }

        public async Task<string> HelloAsync()
        {
            string requestUri = $"{_rootPath}/hello";
            return await GetAsync<string>( requestUri );
        }

        public string WhoAmI() { return WhoAmIAsync().Result; }

        public async Task<string> WhoAmIAsync()
        {
            string requestUri = $"{_rootPath}/hello/whoami";
            return await GetAsync<string>( requestUri );
        }



        public User GetUserByUId(Guid userUId)
        {
            return GetUserByUIdAsync( userUId ).Result;
        }

        public async Task<User> GetUserByUIdAsync(Guid userUId)
        {
            string requestUri = $"{_rootPath}/users/{userUId}";
            return await GetAsync<User>( requestUri );
        }

        public List<User> GetUserByName(string name, bool exact = false)
        {
            throw new NotImplementedException();
        }

        public User UpsertUser(User user)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(Guid userUId)
        {
            throw new NotImplementedException();
        }



        public Group GetGroupByUId(Guid groupUId)
        {
            throw new NotImplementedException();
        }

        public List<Group> GetGroupByName(string name, bool exact = false)
        {
            throw new NotImplementedException();
        }

        public Group UpsertGroup(Group group)
        {
            throw new NotImplementedException();
        }

        public void UpdateSecureObjectParentUId(ISecureObject secureObject, Guid? newParentUId)
        {
            throw new NotImplementedException();
        }

        public void DeleteGroup(Guid groupUId)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<GroupMembershipItem> GetGroupMembers(Guid groupUId, bool includeDisabledMembership = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupMembershipItem> GetGroupMemberOf(Guid memberUId, bool includeDisabledMembership = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<GroupMembershipItem> GetGroupMembershipHierarchy(Guid memberUId, bool includeDisabledMembership = false)
        {
            throw new NotImplementedException();
        }

        public GroupMembershipItem UpsertGroupMembership(GroupMembershipItem groupMembershipItem)
        {
            throw new NotImplementedException();
        }

        public List<GroupMembershipItem> UpsertGroupMembership(List<GroupMembershipItem> groupMembershipItems)
        {
            throw new NotImplementedException();
        }

        public void DeleteGroupMembership(GroupMembershipItem groupMembershipItem)
        {
            throw new NotImplementedException();
        }


        public MembershipList<SecurityPrincipalBase> GetGroupMembersList(Guid groupUId, bool includeDisabledMembership = false)
        {
            throw new NotImplementedException();
        }

        public MembershipList<SecurityPrincipalBase> GetGroupMembersList(Group group, bool includeDisabledMembership = false)
        {
            throw new NotImplementedException();
        }

        public MembershipList<Group> GetGroupMemberOfList(Guid memberUId, bool isMemberGroup = false, bool includeDisabledMembership = false)
        {
            throw new NotImplementedException();
        }

        public MembershipList<Group> GetGroupMemberOfList(SecurityPrincipalBase member, bool includeDisabledMembership = false)
        {
            throw new NotImplementedException();
        }



        public IEnumerable<ISecureObject> GetSecureObjects()
        {
            throw new NotImplementedException();
        }

        public ISecureObject GetSecureObjectByUId(Guid secureObjectUId, bool includeChildren, bool includeDisabled = false)
        {
            throw new NotImplementedException();
        }

        public ISecureObject GetSecureObjectByUniqueName(string uniqueName, bool includeChildren, bool includeDisabled = false)
        {
            throw new NotImplementedException();
        }

        public ISecureObject UpsertSecureObject(ISecureObject secureObject)
        {
            throw new NotImplementedException();
        }

        public void DeleteSecureObject(Guid secureObjectUId)
        {
            throw new NotImplementedException();
        }
    }
}