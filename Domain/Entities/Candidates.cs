using Domain.Common;

namespace Domain.Entities
{
    /// <summary>
    /// Class to represent the Candidates entity
    /// </summary>
    public class Candidates : BaseEntity
    {
        /// <summary>
        /// First Name of the Candidates
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Last Name of the Candidates
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// Phone Nmber of the Candidates
        /// </summary>
        public string? PhoneNmber { get; set; }

        /// <summary>
        /// Call Time prefered by the Candidates
        /// </summary>
        public string? CallTime { get; set; }

        /// <summary>
        /// LinkedIn URL of the Candidates
        /// </summary>
        public string? LinkedInURL { get; set; }

        /// <summary>
        /// GitHub URL of the Candidates
        /// </summary>
        public string? GitHubURL { get; set; }

        /// <summary>
        /// Text Written by the Candidates
        /// </summary>
        public string Text { get; set; }
    }
}
