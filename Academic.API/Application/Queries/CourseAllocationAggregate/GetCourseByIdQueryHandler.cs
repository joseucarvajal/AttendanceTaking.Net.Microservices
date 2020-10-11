using Academic.Domain.CourseAllocationAggregate;
using Academic.Infrastructure;
using App.Common.SeedWork;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Academic.API.Application.Queries.CourseAllocationAggregate
{
    public class GetCourseByIdQueryHandler
    {
        public class Query : IRequest<Course>
        {
            public Query(Guid id)
            {
                Id = id;
            }

            public Guid Id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Course>
        {
            private CommonGlobalAppSingleSettings _commonGlobalAppSingleSettings;

            public Handler(CommonGlobalAppSingleSettings commonGlobalAppSingleSettings)
            {
                _commonGlobalAppSingleSettings = commonGlobalAppSingleSettings;
            }

            public async Task<Course> Handle(Query request, CancellationToken cancellationToken)
            {
                using(IDbConnection conn = new SqlConnection(_commonGlobalAppSingleSettings.MssqlConnectionString))
                {
                    string sql = @"
                        SELECT Id, Code, Name, CourseGroupId
                        FROM courses
                        WHERE Id = @Id";

                    return await conn.QuerySingleOrDefaultAsync<Course>(sql, new { Id = request.Id });
                }
            }
        }
    }
}
