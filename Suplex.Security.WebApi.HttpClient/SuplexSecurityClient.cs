using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Suplex.Security.AclModel;
using Suplex.Security.DataAccess;
using Suplex.Security.Principal;


namespace Suplex.Security.WebApi
{
    public class SuplexSecurityClient : ISuplexDal
    {
        ISuplexDal _dal = null;

        public SuplexSecurityClient(string baseUrl, string messageFormatType = "application/json")
        {
        }


        public string Hello()
        {
            return "Hello";
        }

        public string WhoAmI()
        {
            return "WhoAmI";
        }




        public User GetUserByUId(Guid userUId)
        {
            throw new NotImplementedException();
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