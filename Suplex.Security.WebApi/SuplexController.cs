using System;
using System.Collections.Generic;
using System.IO;
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
            string configFileName = "Suplex.Security.WebApi.config.yaml";

            object config = Synapse.Services.ExtensibilityUtility.GetExecuteControllerInstance( null, null, null )?.GetCustomAssemblyConfig( "Suplex.Security.WebApi" );
            if( config != null )
            {
                SuplexDalConfig suplexDalConfig = SuplexDalConfig.FromObject( config );
                _dal = suplexDalConfig.GetDalInstance();
            }
            else if( File.Exists( configFileName ) )
            {
                string configYaml = File.ReadAllText( configFileName );
                SuplexDalConfig suplexDalConfig = SuplexDalConfig.FromYaml( configYaml );
                _dal = suplexDalConfig.GetDalInstance();
            }
        }

        [HttpGet]
        [Route( "hello" )]
        public string Hello()
        {
            return "Hello from SuplexController, World!";
        }


        #region users
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
        #endregion


        #region groups
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
        #endregion


        #region gm
        [HttpGet]
        [Route( "gm/" )]
        public IEnumerable<GroupMembershipItem> GetGroupMembership()
        {
            return _dal.GetGroupMembership();
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
        public IEnumerable<GroupMembershipItem> UpsertGroupMembership(IEnumerable<GroupMembershipItem> groupMembershipItems)
        {
            return _dal.UpsertGroupMembership( groupMembershipItems );
        }

        [HttpDelete]
        [Route( "gm/{groupUId:Guid}" )]
        public void DeleteGroupMembership(Guid groupUId, Guid memberUId)
        {
            _dal.DeleteGroupMembership( new GroupMembershipItem { GroupUId = groupUId, MemberUId = memberUId } );
        }
        void ISuplexDal.DeleteGroupMembership(GroupMembershipItem groupMembershipItem)
        {
            _dal.DeleteGroupMembership( groupMembershipItem );
        }

        [HttpDelete]
        [Route( "gm/items" )]
        public void DeleteGroupMembership(IEnumerable<GroupMembershipItem> groupMembershipItems)
        {
            _dal.DeleteGroupMembership( groupMembershipItems );
        }
        void ISuplexDal.DeleteGroupMembership(IEnumerable<GroupMembershipItem> groupMembershipItems)
        {
            _dal.DeleteGroupMembership( groupMembershipItems );
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
        #endregion


        #region secure objects
        [HttpGet]
        [Route( "so/all/" )]
        public IEnumerable<ISecureObject> GetSecureObjects()
        {
            return _dal.GetSecureObjects();
        }

        [HttpGet]
        [Route( "so/{secureObjectUId:Guid}" )]
        public SecureObject GetSecureObjectByUId(Guid secureObjectUId, bool includeChildren, bool includeDisabled = false)
        {
            return _dal.GetSecureObjectByUId( secureObjectUId, includeChildren, includeDisabled ) as SecureObject;
        }
        ISecureObject ISuplexDal.GetSecureObjectByUId(Guid secureObjectUId, bool includeChildren, bool includeDisabled)
        {
            return _dal.GetSecureObjectByUId( secureObjectUId, includeChildren, includeDisabled );
        }

        [HttpGet]
        [Route( "so/" )]
        public SecureObject GetSecureObjectByUniqueName(string uniqueName, bool includeChildren, bool includeDisabled = false)
        {
            return _dal.GetSecureObjectByUniqueName( uniqueName, includeChildren, includeDisabled ) as SecureObject;
        }
        ISecureObject ISuplexDal.GetSecureObjectByUniqueName(string uniqueName, bool includeChildren, bool includeDisabled)
        {
            return _dal.GetSecureObjectByUniqueName(uniqueName, includeChildren, includeDisabled);
        }
        [HttpPost]
        [Route( "so/" )]
        public SecureObject UpsertSecureObject([FromBody]SecureObject secureObject)
        {
            return _dal.UpsertSecureObject( secureObject ) as SecureObject;
        }
        ISecureObject ISuplexDal.UpsertSecureObject([FromBody]ISecureObject secureObject)
        {
            return _dal.UpsertSecureObject( secureObject );
        }

        [HttpPut]
        [Route( "so/{newParentUId:Guid?}" )]
        public void UpdateSecureObjectParentUId([FromBody]SecureObject secureObject, Guid? newParentUId = null)
        {
            _dal.UpdateSecureObjectParentUId( secureObject, newParentUId );
        }
        void ISuplexDal.UpdateSecureObjectParentUId(ISecureObject secureObject, Guid? newParentUId)
        {
            _dal.UpdateSecureObjectParentUId( secureObject, newParentUId );
        }

        [HttpPut]
        [Route( "so/uid/{secureObjectUId:Guid}/{newParentUId:Guid?}" )]
        public void UpdateSecureObjectParentUId(Guid secureObjectUId, Guid? newParentUId = null)
        {
            _dal.UpdateSecureObjectParentUId( secureObjectUId, newParentUId );
        }
        void ISuplexDal.UpdateSecureObjectParentUId(Guid secureObjectUId, Guid? newParentUId)
        {
            _dal.UpdateSecureObjectParentUId( secureObjectUId, newParentUId );
        }

        [HttpDelete]
        [Route( "so/{secureObjectUId:Guid}" )]
        public void DeleteSecureObject(Guid secureObjectUId)
        {
            _dal.DeleteSecureObject( secureObjectUId );
        }
        #endregion
    }
}