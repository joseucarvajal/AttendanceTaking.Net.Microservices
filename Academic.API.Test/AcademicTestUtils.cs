using Academic.Domain.CourseAllocationAggregate;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Academic.API.Test
{
    public class AcademicTestUtils
    {
        public async Task<IEnumerable<Course>> GetAllCoursesAsync(HttpClient client)
        {
            var response = await client.GetAsync("/api/v1/CourseAllocation/course/details");
            response.EnsureSuccessStatusCode(); // Status Code 200-299            
            var courseList = JsonConvert.DeserializeObject<IEnumerable<Course>>(await response.Content.ReadAsStringAsync());
            Assert.True(courseList.Count() > 0, "Courses list where not loaded");

            return courseList;
        }

        public async Task<Course> CreateNewCourse_createsNewCourse(HttpClient client, Course course)
        {
            // Arrange

            // Act            
            var httpContent = new StringContent(JsonConvert.SerializeObject(course), Encoding.UTF8, "application/json");
            var response = await client.PostAsync("/api/v1/CourseAllocation/course", httpContent);            

            // Assert
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var existingCourse = JsonConvert.DeserializeObject<Course>(await response.Content.ReadAsStringAsync());
            Assert.True(existingCourse.Id != null, "created course does not have an Id");

            // Clean up
            return course;
        }

        public async Task RemoveCourseAsync(
            HttpClient client, Course course)
        {
            var response = await client.DeleteAsync($"/api/v1/CourseAllocation/course/{course.Id}");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            var courseList = await GetAllCoursesAsync(client);
            Assert.True(courseList.Where(c => c.Id == course.Id).Count() == 0, "Courses was not deleted");
        }
    }
}
