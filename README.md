# Cafe management system with .Net
Note: This project is under development.

Common features:
1. Managing concurrency exceptions for item and user entities
2. Pooled context & Multi-tenancy architectural pattern for managing state in pooled context
3. Unit Of Work pattern & Generic service for CRUD operations
4. Safe against attacks include: mass assignment attack , cross-site request forgery (CSRF)

Admin panel features:
1. Users account management through the database
2. Edit, delete Information of each user(Include: user name, password , phone number)
3. Create new user account
4. Cafe products management through the database
5. Edit, delete any product information(Include: item name, item category, unit price)
6. Sorting based on the heading of the date and name columns and switch between ascending and descending sort order by repeatedly clicks on column heading
7. Search and filter items and users by name
8. Paging users and items with next and previous button
9. Grouping number of users based on sign up time
10. Managing all orders of each user individually with details(Include: User full name, Order date, Order status, Order name, Order category, Order unit price, Order qty, Total)
11. Managing all orders of each item individually with details(Include: Item name, Item Category, Unite price, Orderer id, Order qty, Order date, Order status)
12. Includes a factor to change the price for all items at once (using raw SQL queries)

Client side features:
1. Create user account
2. Order through user account(Username and password must be valid)
3. Management of orders through the account(Basic information include: order number, order date, order time, orderer user name, orderer contact.
Order detail include: item name, item category, item unit price, item quantity, item final amount)
