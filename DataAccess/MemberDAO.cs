using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.Models;


namespace DataAccess
{
    public class MemberDAO
    {
        private static MemberDAO instance = null;
        private static readonly object instanceLock = new object();
        private MemberDAO() { }
        public static MemberDAO Instance
        {
            get
            {
                lock (instanceLock)
                {
                    if (instance == null)
                    {
                        instance = new MemberDAO();
                    }
                    return instance;
                }
            }
        }

        public IEnumerable<Member> GetAllMember()
        {
            FStoreDBContext fStoreDBContext = new FStoreDBContext();
            var memberList = from member in fStoreDBContext.Members select member;
            return memberList.ToList();
        }

        public Member GetmemberByEmail(string email)
        {
            FStoreDBContext fStoreDBContext = new FStoreDBContext();
            var memberList = (from member in fStoreDBContext.Members where member.Email == email select member).FirstOrDefault();
            return memberList;
        }

        public void Insert(Member member)
        {
            try
            {
                using (var fStoreDBContext = new FStoreDBContext())
                {

                    fStoreDBContext.Members.Add(member);
                    fStoreDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in add new member: " + ex.Message);
            }
        }
        public void Update(Member member)
        {
            try
            {
                using (var fStoreDBContext = new FStoreDBContext())
                {
                    fStoreDBContext.Members.Update(member);
                    fStoreDBContext.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error in update member: " + ex.Message);
            }
        }

        public void Delete(Member member)
        {
            try
            {
                using (var fStoreDBContext = new FStoreDBContext())
                {   
                    var mem = fStoreDBContext.Members.SingleOrDefault(m => m.MemberId == member.MemberId);
                    fStoreDBContext.Members.Remove(mem);
                    fStoreDBContext.SaveChanges();
                }
            } catch (Exception ex)
            {
                throw new Exception("Error in delete member: " + ex.Message);
            }
        }

       

        public Member CheckLogin(string email, string password)
        {
            FStoreDBContext fStoreDBContext = new FStoreDBContext();
            return (from member in fStoreDBContext.Members
                    where member.Email.Equals(email) && member.Password.Equals(password)
                    select member).FirstOrDefault();
        }

        public int GetMaxMemberId()
        {
            try
            {
                using(var fStoreDBContext = new FStoreDBContext())
                {
                    int id = (from member in fStoreDBContext.Members
                              orderby member.MemberId descending
                              select member.MemberId).FirstOrDefault();
                    return id;
                }
            } catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}

