using FluentValidation;

namespace TaskManagementSystem.API.Validations
{
    public class CreateTaskValidation: AbstractValidator<Models.Task>
    {
        public CreateTaskValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(x => x.Description).NotEmpty().WithMessage("Description is required");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Status is required")
                                    .Must(x => x == Models.TaskStatus.Pending.ToString() ||
                                     x == Models.TaskStatus.InProgress.ToString() ||
                                      x == Models.TaskStatus.Completed.ToString()).WithMessage("Invalid status");

            RuleFor(x => x.DueDate).NotEmpty().WithMessage("Due date is required");
        }
    }
}