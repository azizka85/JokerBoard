﻿using EQUTech.Core.Grpc.Models.JokerBoard;

namespace EQUTech.Core.Services.JokerBoard;

public interface ICategoryItemService
{
    List<CategoryItem> List();
}
