﻿namespace ViewModels.Response
{
    public class LoginResponse
    {
        public string AccessToken { get; set; } = string.Empty;
        public string RefreshToken { get; set; } = string.Empty;
        public UsersResponse? Users { get; set; }
    }
}
