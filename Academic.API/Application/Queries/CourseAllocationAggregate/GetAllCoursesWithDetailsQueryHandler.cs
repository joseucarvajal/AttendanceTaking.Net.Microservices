using Academic.Domain.CourseAllocationAggregate;
using App.Common.SeedWork;
using Dapper;
using MediatR;
using Microsoft.Data.SqlClient;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Academic.API.Application.Queries.CourseAllocationAggregate
{
    public class GetAllCoursesWithDetailsQueryHandler
    {
        public class Query : IRequest<IEnumerable<Course>>
        {            
        }

        public class Handler : IRequestHandler<Query, IEnumerable<Course>>
        {
            private CommonGlobalAppSingleSettings _commonGlobalAppSingleSettings;

            public Handler(CommonGlobalAppSingleSettings commonGlobalAppSingleSettings)
            {
                _commonGlobalAppSingleSettings = commonGlobalAppSingleSettings;
            }

            public async Task<IEnumerable<Course>> Handle(Query request, CancellationToken cancellationToken)
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

                    var result = await conn
                                    .QueryAsync<Course, CourseGroup, Course>(
                                        sql.ToString(),
                                        (c, cg) => {
                                            c.CourseGroup = cg;
                                            return c;
                                        }
                                 );

                    return result.AsEnumerable();
                }
            }
        }
    }
}
