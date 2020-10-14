using Academic.Domain.CourseAllocationAggregate;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using Xunit;

namespace Academic.API.Test
{
    public class CourseAllocationControllerTest 
        : BaseIntegrationTest<Startup>
    {        
        public CourseAllocationControllerTest(WebApplicationFactory<Startup> factory)
            : base(factory)
        {
            
        }

        [Theory]
        [InlineData("/api/v1/CourseAllocation/course/details")]
        public async void GetAllCourseDetails_getsAllCourses(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var courseList = JsonConvert
                                .DeserializeObject<IEnumerable<Course>>
                                    (await response.Content.ReadAsStringAsync());

            Assert.True(courseList.Count() > 0, "Courses list where not loaded");
        }

        [Fact]
        public async void GetCourseById_GetsTheExpectedCourse()
        {
            // Arrange
            var client = Factory.CreateClient();
            var courseList = await AcademicTestUtils.GetAllCoursesAsync(client);

            // Act
                // Assert search existing course gets the right course
            var response = await client.GetAsync($"/api/v1/CourseAllocation/course/{courseList.ElementAt(0).Id}");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var courseFound = JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());
            Assert.True(courseFound.Id == courseList.ElementAt(0).Id, "Course by id was not found");

                // Assert NOT existing course, returns null
            response = await client.GetAsync($"/api/v1/CourseAllocation/course/1741D3FF-8FD3-4DC4-4678-08D86FC81FE7");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            courseFound = JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());
            Assert.True(courseFound == null, "Course was not null");
        }

        [Theory]
        [InlineData("/api/v1/CourseAllocation/course")]
        public async void CreateNewCourse_createsNewCourse(string url)
        {
            // Arrange
            var client = Factory.CreateClient();
            Course course = new Course
            {
                Code = "new-course",
                Name = "new test course"
            };
            var httpContent = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");

            // Act
            var response = await client.PostAsync(url, httpContent);            

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var existingCourse = JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());
            Assert.True(existingCourse.Id != null, "created course does not have an Id");

            // Clean up
            await AcademicTestUtils.RemoveCourseAsync(client, existingCourse);
        }
    }
}
