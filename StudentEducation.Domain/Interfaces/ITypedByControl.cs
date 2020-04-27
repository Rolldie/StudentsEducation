using StudentsEducation.Domain.Entities;

namespace StudentsEducation.Domain.Interfaces
{
    public interface ITypedByControl
    {
        public ControlType GetControlType();
    }
}