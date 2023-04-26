using Core.Dtos;
using DataLayer;
using DataLayer.Entities;

namespace Core.Services
{
    public class UserService
    {
        private readonly UnitOfWork unitOfWork;
        private AuthorizationService authService { get; set; }

        public UserService(UnitOfWork unitOfWork, AuthorizationService authService)
        {
            this.unitOfWork = unitOfWork;
            this.authService = authService;
        }


        public UserAddDto RegisterStudent(UserAddDto payload)
        {
            if (payload == null) return null;

            var hasNameConflict = unitOfWork.Users
                .Any(u=>u.UserName == payload.UserName);

            if (hasNameConflict) return null;

            var hashedPassword = authService.HashPassword(payload.Password);

            var user = new User
            {
                Password = hashedPassword,
                UserName = payload.UserName,
                Role = new Role
                {
                    Id = 1,
                    Description="Student"
                }
            };

            unitOfWork.Users.Insert(user);
            unitOfWork.SaveChanges();

            return payload;
        }

        public UserAddDto RegisterProfessor(UserAddDto payload)
        {
            if (payload == null) return null;

            var hasNameConflict = unitOfWork.Users
                .Any(u => u.UserName == payload.UserName);

            if (hasNameConflict) return null;

            var hashedPassword = authService.HashPassword(payload.Password);

            var user = new User
            {
                Password = hashedPassword,
                UserName = payload.UserName,
                Role = new Role
                {
                    Id = 2,
                    Description = "Professor"
                }
            };

            unitOfWork.Users.Insert(user);
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

        public string GetRole(User user)
        {
            return user.Role.Description;
        }

        public string Validate(UserAddDto payload)
        {
            var user = unitOfWork.Users.GetByUsername(payload.UserName);

            var passwordFine = authService.VerifyHashedPassword(user.Password, payload.Password);

            if (passwordFine)
            {
                var role = GetRole(user);

                return authService.GetToken(user, role);
            }
            else
            {
                return null;
            }

        }
    }
}
