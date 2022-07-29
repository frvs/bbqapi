using BarbequeApi.Models.Dtos;
using System.Collections.Generic;

namespace BarbequeApi.Validators
{
  public abstract class AbstractValidator<T> where T : BaseDto
  {
    public abstract (bool, List<string>) Validate(T entity);
  }
}
