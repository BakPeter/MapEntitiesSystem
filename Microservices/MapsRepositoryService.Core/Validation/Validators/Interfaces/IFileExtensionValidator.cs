﻿using MapsRepositoryService.Core.Model;

namespace MapsRepositoryService.Core.Validation.Validators.Interfaces;

public interface IFileExtensionValidator
{
    ResultModel Validate(MapModel mapModel);
}