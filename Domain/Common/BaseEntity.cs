namespace Domain.Common
{
    /// <summary>
    /// Class to represent the entities default fields (all the entties types will have those fields by default)
    /// </summary>
    public class BaseEntity
    {
        /// <summary>
        /// Id of the record
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// User who created the record
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Date when the record was created
        /// </summary>
        public DateTime CreatedOn { get; set; }

        /// <summary>
        /// User who lastly updated the record
        /// </summary>
        public string? LastModifiedBy { get; set; }

        /// <summary>
        /// Date when the record was lastly updated
        /// </summary>
        public DateTime? LastModifiedOn { get; set; }

        /// <summary>
        /// User who deleted the record
        /// </summary>
        public string? DeletedBy { get; set; }

        /// <summary>
        /// Date when the record was deleted
        /// </summary>
        public DateTime? DeletedOn { get; set; }

        /// <summary>
        /// Bool wether the record is deleted or not
        /// </summary>
        public bool? IsDeleted { get; set; }
    }
}
