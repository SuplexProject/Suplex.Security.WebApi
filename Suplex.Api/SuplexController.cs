using System;
using System.Collections.Generic;
using System.Web.Http;
using Suplex.Security.AclModel;
using Suplex.Security.DataAccess;
using Suplex.Security.Principal;

namespace Suplex.Security.WebApi
{
    [RoutePrefix( "custom" )]
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


        //[HttpGet]
        //[Route( "GetUserByUId" )]
        //public User GetUserByUId(Guid userUId)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route( "GetUserByName" )]
        //public List<User> GetUserByName(string name)
        //{
        //    throw new NotImplementedException();
        //}

        //public User UpsertUser(User user)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteUser(Guid userUId)
        //{
        //    throw new NotImplementedException();
        //}


        //[HttpGet]
        //[Route( "GetGroupByUId" )]
        //public Group GetGroupByUId(Guid groupUId)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route( "GetGroupByName" )]
        //public List<Group> GetGroupByName(string name)
        //{
        //    throw new NotImplementedException();
        //}

        //public Group UpsertGroup(Group group)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteGroup(Guid groupUId)
        //{
        //    throw new NotImplementedException();
        //}


        //[HttpGet]
        //[Route( "GetGroupMemberOf" )]
        //public IEnumerable<GroupMembershipItem> GetGroupMemberOf(Guid memberUId, bool includeDisabledMembership = false)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route( "GetGroupMembers" )]
        //public IEnumerable<GroupMembershipItem> GetGroupMembers(Guid groupUId, bool includeDisabledMembership = false)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route( "GetGroupMembershipHierarchy" )]
        //public IEnumerable<GroupMembershipItem> GetGroupMembershipHierarchy(Guid memberUId, bool includeDisabledMembership = false)
        //{
        //    throw new NotImplementedException();
        //}

        //public GroupMembershipItem UpsertGroupMembership(GroupMembershipItem groupMembershipItem)
        //{
        //    throw new NotImplementedException();
        //}

        //public List<GroupMembershipItem> UpsertGroupMembership(List<GroupMembershipItem> groupMembershipItems)
        //{
        //    throw new NotImplementedException();
        //}

        //public void DeleteGroupMembership(GroupMembershipItem groupMembershipItem)
        //{
        //    throw new NotImplementedException();
        //}


        //[HttpGet]
        //[Route( "GetGroupMembershipList" )]
        //public MembershipList<SecurityPrincipalBase> GetGroupMembershipList(Group group, bool includeDisabledMembership = false)
        //{
        //    throw new NotImplementedException();
        //}

        //[HttpGet]
        //[Route( "GetGroupMembershipListOf" )]
        //public MembershipList<Group> GetGroupMembershipListOf(SecurityPrincipalBase member, bool includeDisabledMembership = false)
        //{
        //    throw new NotImplementedException();
        //}



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