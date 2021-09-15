using EPM.Model.ApiModel;
using EPM.Model.DbModel;
using EPM.Model.Dto.Response.UserResponse;
using EPM.Model.Enum;
using EPM.UserMicroService.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EPM.UserMicroService.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<int> Add(User entity)
        {
            _dbContext.Users.Add(entity);
            return await _dbContext.SaveChangesAsync();
        }

        public async Task<IEnumerable<User>> GetAllListAsync(Expression<Func<User, bool>> predicate)
        {
            return predicate != null ? await _dbContext.Users.Where(predicate).ToListAsync() : await _dbContext.Users.ToListAsync();
        }

        public async Task<User> GetEntityAsync(Expression<Func<User, bool>> predicate)
        {
            return predicate != null ? await _dbContext.Users.FirstOrDefaultAsync(predicate) : await _dbContext.Users.FirstOrDefaultAsync();
        }

        public async Task<UserResponseDto> GetPatgeListAsync(PagingRequest pagingRequest)
        {
            UserResponseDto responseDto = new UserResponseDto();
            var query = from u in _dbContext.Users
                        where u.IsDeleted == (int)DeleteFlag.NotDeleted
                        select new User
                        {
                            ClusterID = u.ClusterID,
                            ID = u.ID,
                            CreateTime = u.CreateTime,
                            CreateUser = u.CreateUser,
                            UpdateTime = u.UpdateTime,
                            UpdateUser = u.UpdateUser,
                            Name = u.Name,
                            LoginName = u.LoginName,
                            Password = u.Password,
                            MobileNumber = u.MobileNumber,
                            EmailAddress = u.EmailAddress,
                            Position = u.Position,
                            Status = u.Status,
                            LoginTime = u.LoginTime,
                            LoginErrorCount = u.LoginErrorCount,
                            RoleName = u.RoleName,
                            DepartmentName = u.DepartmentName,
                            Description = u.Description,
                            IsDeleted = u.IsDeleted
                        };

            // 分页查询
            if (pagingRequest.IsPaging)
            {
                var skip = (pagingRequest.PageIndex - 1) * pagingRequest.PageSize;
                responseDto.ResponseData = await query
                    .Skip(skip).Take(pagingRequest.PageSize).ToListAsync();
            }
            else
            {
                responseDto.ResponseData = await query.ToListAsync();
            }

            responseDto.Count = query.Count();
            return responseDto;
        }

        public async  Task<int> Update(User entity, Expression<Func<User, object>>[] updatedProperties)
        {
            _dbContext.Set<User>().Attach(entity);

                foreach (var property in updatedProperties)
                {
                    _dbContext.Entry(entity).Property(property).IsModified = true;
                }
                return await _dbContext.SaveChangesAsync();

        }
    }
}
