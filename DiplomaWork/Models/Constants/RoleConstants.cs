using System;

namespace DiplomaWork1.Models.Constants
{
    public class RoleConstants
    {
        public const string Admin = nameof(Admin);
        public static Guid AdminId = new Guid("ffce5e46-7143-454d-9d8b-9b7ba5bc5488");

        public const string TechnicalSpecialist = "Технических специалист";
        public static Guid TechnicalSpecialistId = new Guid("ccc481e1-739c-474c-a64d-432244fce57d");

        public const string HrManager = "Отдел кадров";
        public static Guid HrManagerId = new Guid("1ddea4f0-3ce3-40e2-a668-8281fbce96c2");

        public const string QualityControl = "Контроль качества";
        public static Guid QualityControlId = new Guid("4f55cdd8-6c38-43ee-89c2-8d89b7680f0e");
    }
}
