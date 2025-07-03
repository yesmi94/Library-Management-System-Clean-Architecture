// <copyright file="ResultClass.cs" company="Ascentic">
// Copyright (c) Ascentic. All rights reserved.
// </copyright>

namespace LibraryManagementCleanArchitecture.Application
{
    public class Result
    {
        public bool IsSuccess { get; set; }

        public bool IsFailure => !IsSuccess;

        public string? Error { get; set; }

        public static Result Success() => new () { IsSuccess = true };

        public static Result Failure(string error) => new () { IsSuccess = false, Error = error };

    }

    public class Result<T> : Result
    {
        public T? Value { get; set; }

        public static new Result<T> Success(T value) => new() { IsSuccess = true, Value = value };

        public static new Result<T> Failure(string error) => new() { IsSuccess = false, Error = error };
    }
}
