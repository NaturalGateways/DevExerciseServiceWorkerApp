using System;

namespace NG.ServiceWorker.ApiModel
{
    public class JobWithLinks
    {
        public Contact Contact { get; set; }

        public Job Job { get; set; }
    }

    public class Job
    {
        public string JobKey { get; set; }

        public int JobStatusId { get; set; }

        public string Description { get; set; }

        public int PaymentTypeId { get; set; }

        public double? PaymentAmount { get; set; }

        public JobScheduleWindow ScheduleWindow { get; set; }

        public int? EstimatedTimeMinutes { get; set; }

        public JobEstimatedMaterial[] EstimatedMaterials { get; set; }
    }

    public class JobScheduleWindow
    {
        public DateTime? StartDateTimeUtc { get; set; }

        public DateTime? EndDateTimeUtc { get; set; }
    }

    public class JobEstimatedMaterial
    {
        public int EquipmentTypeId { get; set; }

        public string EquipmentTypeName { get; set; }

        public int? Amount { get; set; }
    }
}
