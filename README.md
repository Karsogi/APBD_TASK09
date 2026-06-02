How to run the application

1. Run migrations:


dotnet ef database update


2. Start the application:


dotnet run


3. Open the browser and navigate to:


/Account/Register




How to create a test user

1. Open `/Account/Register`.
2. Enter email and password.
3. Submit the registration form.
4. Log in using the created account.


How to log in as Admin

Email:


admin@test.com


Password:


Admin123!


Where password hashing code is

`Controllers/AccountController.cs`

Uses:


PasswordHasher<AppUser>


 Where authentication is configured

`Program.cs`

Uses Cookie Authentication.


Actions protected with [Authorize]

Dashboard:

[Authorize]
DashboardController

Admin:


[Authorize(Roles = "Admin")]
AdminController



Questions

 1. Why must passwords not be stored as plain text?

Database leak exposes passwords.

2. Why is raw SHA-256 not a good choice for passwords?

Too fast for passwords.

3. Why do we use salt?

Prevents identical hashes.

4. What is the difference between salt and pepper?

Salt per user, pepper global.

5. What is the difference between authentication and authorization?

Authentication identifies, authorization permits.

6. Why is hiding a link in a view not enough as security?

URL remains accessible.

7. Why can a "there is no such user" login message be a problem?

Reveals existing accounts.
