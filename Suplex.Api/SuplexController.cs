using System;
using System.Collections.Generic;
using System.Web.Http;
using Suplex.Security.AclModel;
using Suplex.Security.DataAccess;
using Suplex.Security.Principal;

namespace Suplex.Security.WebApi
{
    [RoutePrefix( "suplex" )]
    public class SuplexController : ApiController, ISuplexDal
    {
        ISuplexDal _dal = null;

        public SuplexController()
        {
            object config = Synapse.Services.ExtensibilityUtility.GetExecuteControllerInstance( null, null, null )?.GetCustomAssemblyConfig( "Suplex.Security.WebApi" );
            SuplexDalConfig suplexDalConfig = SuplexDalConfig.FromObject( config );
            _dal = suplexDalConfig.GetDalInstance();
        }

        [HttpGet]
        [Route( "hello" )]
        public string Hello()
        {
            return "Hello from SuplexController, World!";
        }


        [HttpGet]
        [Route( "users/{userUId:Guid}/" )]
        public User GetUserByUId(Guid userUId)
        {
            return _dal.GetUserByUId( userUId );
        }

        [HttpGet]
        [Route( "users/" )]
        public List<User> GetUserByName(string name = null, bool exact = false)
        {
            return _dal.GetUserByName( name, exact );
        }

        [HttpPost]
        [Route( "users/" )]
        public User UpsertUser([FromBody]User user)
        {
            return _dal.UpsertUser( user );
        }

        [HttpDelete]
        [Route( "users/{userUId:Guid}/" )]
        public void DeleteUser(Guid userUId)
        {
            _dal.DeleteUser( userUId );
        }


        [HttpGet]
        [Route( "groups/{groupUId:Guid}" )]
        public Group GetGroupByUId(Guid groupUId)
        {
            return _dal.GetGroupByUId( groupUId );
        }

        [HttpGet]
        [Route( "groups/" )]
        public List<Group> GetGroupByName(string name, bool exact = false)
        {
            return _dal.GetGroupByName( name, exact );
        }

        [HttpPost]
        [Route( "groups/" )]
        public Group UpsertGroup(Group group)
        {
            return _dal.UpsertGroup( group );
        }

        [HttpDelete]
        [Route( "groups/{groupUId:Guid}/" )]
        public void DeleteGroup(Guid groupUId)
        {
            _dal.DeleteGroup( groupUId );
        }


        [HttpGet]
        [Route( "gm/{groupUId:Guid}/members" )]
        public IEnumerable<GroupMembershipItem> GetGroupMembers(Guid groupUId, bool includeDisabledMembership = false)
        {
            return _dal.GetGroupMembers( groupUId, includeDisabledMembership );
        }

        [HttpGet]
        [Route( "gm/{memberUId:Guid}/memberof" )]
        public IEnumerable<GroupMembershipItem> GetGroupMemberOf(Guid memberUId, bool includeDisabledMembership = false)
        {
            return _dal.GetGroupMemberOf( memberUId, includeDisabledMembership );
        }

        [HttpGet]
        [Route( "gm/{memberUId:Guid}/hier" )]
        public IEnumerable<GroupMembershipItem> GetGroupMembershipHierarchy(Guid memberUId, bool includeDisabledMembership = false)
        {
            return _dal.GetGroupMembershipHierarchy( memberUId, includeDisabledMembership );
        }

        [HttpPost]
        [Route( "gm/" )]
        public GroupMembershipItem UpsertGroupMembership(GroupMembershipItem groupMembershipItem)
        {
            return _dal.UpsertGroupMembership( groupMembershipItem );
        }

        [HttpPost]
        [Route( "gm/items/" )]
        public List<GroupMembershipItem> UpsertGroupMembership(List<GroupMembershipItem> groupMembershipItems)
        {
            return _dal.UpsertGroupMembership( groupMembershipItems );
        }

        [HttpDelete]
        [Route( "gm/" )]
        public void DeleteGroupMembership([FromBody]GroupMembershipItem groupMembershipItem)
        {
            _dal.DeleteGroupMembership( groupMembershipItem );
        }


        [HttpGet]
        [Route( "gm/ml/{groupUId:Guid}/members" )]
        public MembershipList<SecurityPrincipalBase> GetGroupMembersList(Guid groupUId, bool includeDisabledMembership = false)
        {
            return _dal.GetGroupMembersList( new Group { UId = groupUId }, includeDisabledMembership );
        }

        MembershipList<SecurityPrincipalBase> ISuplexDal.GetGroupMembersList(Group group, bool includeDisabledMembership)
        {
            return _dal.GetGroupMembersList( group, includeDisabledMembership );
        }

        [HttpGet]
        [Route( "gm/ml/{memberUId:Guid}/memberof" )]
        public MembershipList<Group> GetGroupMemberOfList(Guid memberUId, bool isMemberGroup = false, bool includeDisabledMembership = false)
        {
            SecurityPrincipalBase member = isMemberGroup ? new Group { UId = memberUId } as SecurityPrincipalBase : new User { UId = memberUId } as SecurityPrincipalBase;
            return _dal.GetGroupMemberOfList( member, includeDisabledMembership );
        }

        MembershipList<Group> ISuplexDal.GetGroupMemberOfList(SecurityPrincipalBase member, bool includeDisabledMembership)
        {
            return _dal.GetGroupMemberOfList( member, includeDisabledMembership );
        }


        [HttpGet]
        [Route( "so/all/" )]
        public IEnumerable<ISecureObject> GetSecureObjects()
        {
            return _dal.GetSecureObjects();
        }

        [HttpGet]
        [Route( "so/{secureObjectUId:Guid}" )]
        public ISecureObject GetSecureObjectByUId(Guid secureObjectUId, bool includeChildren, bool includeDisabled = false)
        {
            return _dal.GetSecureObjectByUId( secureObjectUId, includeChildren, includeDisabled );
        }

        [HttpGet]
        [Route( "so/" )]
        public ISecureObject GetSecureObjectByUniqueName(string uniqueName, bool includeChildren, bool includeDisabled = false)
        {
            return _dal.GetSecureObjectByUniqueName( uniqueName, includeChildren, includeDisabled );
        }

        [HttpPost]
        [Route( "so/" )]
        public ISecureObject UpsertSecureObject([FromBody]ISecureObject secureObject)
        {
            return _dal.UpsertSecureObject( secureObject );
        }

        [HttpPut]
        [Route( "so/{newParentUId:Guid}" )]
        public void UpdateSecureObjectParentUId(ISecureObject secureObject, Guid? newParentUId)
        {
            _dal.UpdateSecureObjectParentUId( secureObject, newParentUId );
        }

        [HttpDelete]
        [Route( "so/{secureObjectUId:Guid}" )]
        public void DeleteSecureObject(Guid secureObjectUId)
        {
            _dal.DeleteSecureObject( secureObjectUId );
        }
    }
}