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
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Academic.API.Application.Queries.CourseAllocationAggregate
{
    public class GetCourseDetailsByIdQueryHandler
    {
        public class Query : IRequest<Course>
        {
            public Query(Guid? id)
            {
                Id = id;
            }

            public Guid? Id { get; set; }
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
                    StringBuilder sql = new StringBuilder(@"
                        SELECT c.Id,
                          c.Code,
                          c.Name,
                          c.CourseGroupId,
	                      cg.Id,
	                      cg.Name
	                    FROM course AS c
		                    LEFT JOIN coursegroup AS cg ON c.CourseGroupId = cg.Id");

                    DynamicParameters parameter = new DynamicParameters();
                    if (request.Id != null)
                    {
                        sql.Append(" WHERE c.Id = @Id");
                        parameter.Add("Id", request.Id);
                    }

                    var result = await conn
                                    .QueryAsync<Course, CourseGroup, Course>(
                                        sql.ToString(),
                                        (c, cg) => {
                                            c.CourseGroup = cg;
                                            return c;
                                        },
                                        new { Id = request.Id }
                                 );

                    return result.SingleOrDefault();
                }
            }
        }
    }
}
