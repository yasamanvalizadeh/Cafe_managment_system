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

# Project Images
![Screenshot (458)](https://user-images.githubusercontent.com/112771618/230287159-252489ed-570e-4ca6-83c4-5cb17fa3e4f3.png)
![Screenshot (459)](https://user-images.githubusercontent.com/112771618/230288955-a3237f67-81db-4ebc-84f3-73e31e1253b2.png)
![Screenshot (433)](https://user-images.githubusercontent.com/112771618/227704849-11f51d7c-25db-49ce-85eb-aa6f7679e07c.png)
![Screenshot (434)](https://user-images.githubusercontent.com/112771618/227704856-406f395a-c815-4077-aa7e-2a0de29bf949.png)
![Screenshot (435)](https://user-images.githubusercontent.com/112771618/227704862-c2bc9833-48dc-4e0a-a8e8-a622bc77edcf.png)
![Screenshot (436)](https://user-images.githubusercontent.com/112771618/227704866-fa67681e-d9ce-45e0-8604-5af1099645fa.png)
![Screenshot (437)](https://user-images.githubusercontent.com/112771618/227704868-81638b0c-5eec-47cf-8433-e7952c711515.png)
![Screenshot (438)](https://user-images.githubusercontent.com/112771618/227704874-3cee0ac4-af84-4ab5-a3cc-61fa0bee80f3.png)
![Screenshot (440)](https://user-images.githubusercontent.com/112771618/227704893-5d68308e-03f3-4cad-853b-4d78899010e6.png)
![Screenshot (441)](https://user-images.githubusercontent.com/112771618/227704902-b78fedfb-e468-4fe7-9f3d-cf6fe7d36214.png)
![Screenshot (442)](https://user-images.githubusercontent.com/112771618/227704903-5c98b45f-d1e0-4f0a-9bcf-031281736782.png)
![Screenshot (443)](https://user-images.githubusercontent.com/112771618/227704904-47baf12a-73e7-4afc-9541-009b8dad5235.png)
![Screenshot (444)](https://user-images.githubusercontent.com/112771618/227704907-c6141b4d-9878-4393-80f8-86e29665080a.png)
![Screenshot (445)](https://user-images.githubusercontent.com/112771618/227704910-8bdbbdf4-bea3-4c18-8270-3e397a91ef24.png)
![Screenshot (446)](https://user-images.githubusercontent.com/112771618/227704912-7b97a8c1-850b-4482-848d-884ff8d94d44.png)
![Screenshot (447)](https://user-images.githubusercontent.com/112771618/227704914-b07095d6-5318-4315-8050-45ca47ab393d.png)
