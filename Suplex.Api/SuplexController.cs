using System;
using System.Collections.Generic;
using System.Web.Http;
using Suplex.Security.AclModel;
using Suplex.Security.DataAccess;
using Suplex.Security.Principal;

namespace Suplex.Security.WebApi
{
    [RoutePrefix( "suplex" )]
    public class SuplexController : ApiController   //, IDataAccessLayer
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

        [HttpGet]
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
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route( "gm/{memberUId:Guid}/memberof" )]
        public IEnumerable<GroupMembershipItem> GetGroupMemberOf(Guid memberUId, bool includeDisabledMembership = false)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route( "gm/{memberUId:Guid}/hier" )]
        public IEnumerable<GroupMembershipItem> GetGroupMembershipHierarchy(Guid memberUId, bool includeDisabledMembership = false)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route( "gm/" )]
        public GroupMembershipItem UpsertGroupMembership(GroupMembershipItem groupMembershipItem)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        [Route( "gm/items/" )]
        public List<GroupMembershipItem> UpsertGroupMembership(List<GroupMembershipItem> groupMembershipItems)
        {
            throw new NotImplementedException();
        }

        [HttpDelete]
        [Route( "gm/" )]
        public void DeleteGroupMembership(GroupMembershipItem groupMembershipItem)
        {
            throw new NotImplementedException();
        }


        [HttpGet]
        [Route( "gm/" )]
        public MembershipList<SecurityPrincipalBase> GetGroupMembershipList(Group group, bool includeDisabledMembership = false)
        {
            throw new NotImplementedException();
        }

        [HttpGet]
        [Route( "gm/" )]
        public MembershipList<Group> GetGroupMembershipListOf(SecurityPrincipalBase member, bool includeDisabledMembership = false)
        {
            throw new NotImplementedException();
        }



        //[HttpGet]
        //[Route( "GetSecureObjectByUId" )]
        //public ISecureObject GetSecureObjectByUId(Guid secureObjectUId, bool includeChildren, bool includeDisabled = false)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route( "GetSecureObjectByUniqueName" )]
        //public ISecureObject GetSecureObjectByUniqueName(string uniqueName, bool includeChildren, bool includeDisabled = false)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route( "UpsertSecureObject" )]
        //public ISecureObject UpsertSecureObject(ISecureObject secureObject)
        //{
        //    throw new NotImplementedException();
        //}

        //public void UpdateSecureObjectParentUId(ISecureObject secureObject, Guid? newParentUId)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteSecureObject(Guid secureObjectUId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}