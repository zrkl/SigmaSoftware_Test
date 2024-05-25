namespace Application.DTO
{
    /// <summary>
    /// Class to represent the DTO default fields (all the DTO types will have those fields by default)
    /// </summary>
    public class BaseDTO
    {
        /// <summary>
        /// Id of the DTO record
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User who created the record
        /// </summary>
        public string? CreatedBy { get; set; }
    }
}
