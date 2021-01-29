﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cofoundry.Domain;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace Cofoundry.Web.Identity
{
    /// <summary>
    /// A helper class with shared functionality between controllers
    /// that manage user login.
    /// </summary>
    public interface IAuthenticationControllerHelper<TUserArea>
        where TUserArea : IUserAreaDefinition
    {

        #region auth

        /// <summary>
        /// Attempts to authenticate the login request, returning the result. This
        /// does not log the user in and can be used instead of LogUserInAsync when 
        /// you want more control over the login workflow. 
        /// 
        /// ModelState is first checked to be valid before checking the auth data against 
        /// the database. An auth errors are added to the ModelState.
        /// </summary>
        /// <param name="controller">
        /// This method is intended to be called from an MVC controller and this
        /// should be the controller instance.
        /// </param>
        /// <param name="viewModel">The view-model data posted to the action.</param>
        /// <returns>The result of the authentication check, this should never be null.</returns>
        Task<UserLoginInfoAuthenticationResult> AuthenticateAsync(Controller controller, ILoginViewModel viewModel);

        /// <summary>
        /// Logs in a user that has already been authenticated, typically
        /// by the AuthenticateAsync method.
        /// </summary>
        /// <param name="controller">
        /// This method is intended to be called from an MVC controller and this
        /// should be the controller instance.
        /// </param>
        /// <param name="user">A UserLoginInfo object that has been returned from a sucessful authentication request.</param>
        /// <param name="rememberUser">
        /// True if the user should stay logged in perminantely; false
        /// if the user should only stay logged in for the duration of
        /// the browser session.
        /// </param>
        Task LogUserInAsync(Controller controller, UserLoginInfo user, bool rememberUser);

        /// <summary>
        /// Retreives the ASP.NET MVC standard "ReturnUrl" query parameter and
        /// validates it to be a local url before returning it.
        /// </summary>
        /// <param name="controller">
        /// This method is intended to be called from an MVC controller and this
        /// should be the controller instance.
        /// </param>
        /// <returns>The return url if it is a valid local url; otherwise null.</returns>
        string GetAndValidateReturnUrl(Controller controller);

        #endregion

        #region ChangePasswordAsync

        /// <summary>
        /// Used to change a users password when it is required before login. Once
        /// completed the user should be redirected back to login to re-authenticate.
        /// </summary>
        /// <param name="controller">
        /// This method is intended to be called from an MVC controller and this
        /// should be the controller instance.
        /// </param>
        /// <param name="vm">The view-model containing the data entered by the user.</param>
        Task ChangePasswordAsync(
            Controller controller,
            IChangePasswordViewModel vm
            );

        #endregion

        #region log out

        /// <summary>
        /// Signs the user out of the user area.
        /// </summary>
        Task LogoutAsync();

        #endregion

        #region forgot password

        /// <summary>
        /// Checks the ModelState is valid and then initiates
        /// a password reset request.
        /// </summary>
        /// <param name="controller">
        /// This method is intended to be called from an MVC controller and this
        /// should be the controller instance.
        /// </param>
        /// <param name="vm">The view-model data posted to the action.</param>
        /// <param name="resetUrlBase">
        /// The relative base path used to construct the reset url 
        /// e.g. new Uri("/auth/forgot-password").
        /// </param>
        Task SendPasswordResetNotificationAsync(
            Controller controller,
            IForgotPasswordViewModel vm,
            Uri resetUrlBase
            );

        /// <summary>
        /// Parses the password reset authentication parameters out of the request
        /// url and validates them against the database before returning the result.
        /// </summary>
        /// <param name="controller">
        /// This method is intended to be called from an MVC controller and this
        /// should be the controller instance.
        /// </param>
        /// <returns>
        /// An object containing the validation result, details of any errors
        /// and the parsed authentication data.
        /// </returns>
        Task<PasswordResetRequestValidationResult> ParseAndValidatePasswordResetRequestAsync(
            Controller controller
            );

        /// <summary>
        /// Completes a password reset, validating the ModelState and
        /// view-model data before updating the database and sending
        /// a confirmation notification.
        /// </summary>
        /// <param name="controller">
        /// This method is intended to be called from an MVC controller and this
        /// should be the controller instance.
        /// </param>
        /// <param name="vm">The view-model data posted to the action.</param>
        Task CompletePasswordResetAsync(
            Controller controller,
            ICompletePasswordResetViewModel vm
            );

        #endregion
    }
}