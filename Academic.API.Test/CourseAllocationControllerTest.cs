using Academic.Domain.CourseAllocationAggregate;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace Academic.API.Test
{
    public class CourseAllocationControllerTest 
        : BaseDbIntegrationTest<Startup>
    {        
        public CourseAllocationControllerTest(WebApplicationFactory<Startup> factory)
            : base(factory)
        {
            
        }

        [Theory]
        [InlineData("/api/v1/CourseAllocation/course/details")]
        public async void GetAllCourseDetails(string url)
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync(url);

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var courseList = JsonConvert.DeserializeObject<IEnumerable<Course>>(await response.Content.ReadAsStringAsync());
            Assert.True(courseList.Count() > 0, "Courses list where not loaded");
        }

        [Fact]        
        public async void GetCourseById()
        {
            // Arrange
            var client = Factory.CreateClient();

            // Act
            var response = await client.GetAsync("/api/v1/CourseAllocation/course/details");

            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var courseList = JsonConvert.DeserializeObject<IEnumerable<Course>>(await response.Content.ReadAsStringAsync());

            // Assert search existing course gets the right course
            response = await client.GetAsync($"/api/v1/CourseAllocation/course/{courseList.ElementAt(0).Id}");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var courseFound = JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());
            Assert.True(courseFound.Id == courseList.ElementAt(0).Id, "Course by id was not found");

            // Assert NOT existing course, returns null
            response = await client.GetAsync($"/api/v1/CourseAllocation/course/1741D3FF-8FD3-4DC4-4678-08D86FC81FE7");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            courseFound = JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());
            Assert.True(courseFound == null, "Course by id was not found");
        }
    }
}
