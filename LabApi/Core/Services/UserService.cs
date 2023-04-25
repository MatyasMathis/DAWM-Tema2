using Core.Dtos;
using DataLayer;
using DataLayer.Entities;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;

        public UserService(UnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
        }


        public UserAddDto AddStudent(UserAddDto payload)
        {
            if (payload == null) return null;

            var hasNameConflict = unitOfWork.Users
                .Any(u=>u.UserName == payload.UserName);

            if (hasNameConflict) return null;

            var newUser = new User
            {
                UserName = payload.UserName,
                Password=payload.Password,
                Role = new Role
                {
                    Id = 2,
                    Description = "Student"
                }
            };

            unitOfWork.Users.Insert(newUser);
            unitOfWork.SaveChanges();

            return payload;
        }

        public UserAddDto AddProfessor(UserAddDto payload)
        {
            if (payload == null) return null;

            var hasNameConflict = unitOfWork.Users
                .Any(u => u.UserName == payload.UserName);

            if (hasNameConflict) return null;

            var newUser = new User
            {
                UserName = payload.UserName,
                Password = payload.Password,
                Role = new Role
                {
                    Id = 1,
                    Description = "Professor"
                }
            };

            unitOfWork.Users.Insert(newUser);
            unitOfWork.SaveChanges();

            return payload;
        }

        public List<UserAddDto> GetAll()
        {
            var users = unitOfWork.Users.GetAll();


            return users.Select(c => new UserAddDto
            {
                UserName= c.UserName,
                Password= c.Password
            }).ToList();
        }
    }
}
