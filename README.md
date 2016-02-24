#Tech Blog - web application for news sharing

##3 user roles

* Administrator - can delete/edit all articles & comments
* PowerUser - can upload articles, can edit articles, which the user previously created; can up/down vote & post comments
* User - can browse articles, up/down vote and post comments
* Unregistered users can only view the articles & posts

All users can search in the articles in the application

##Database models
* Articles - generated into several fixed categories
* Comments - comments for each articles
* Votes - votes for each article. Each user can vote only once per article. He/she can change the vote later

##Index page
* Contains a slider, showing the latest 4 articles; The page is cached for 15 minutes
* Main contain shows the 6 top articles (by votes count

![alt tag](/screenshots/screen1.jpeg)

##Category page
* Shows articles from each category, with server side paging. Two articles per page, with sample text and link to articles. Registered users can vote for the articles
* In the aside element there are the "hot" articles section - showing the top 6 recent articles and the "From the archives" section, showing the 6 best rated articles

##Article details page
* Shows the whole article & its comments. Registered users can add comments here.
* In the aside element there are the "hot" articles section - showing the top 6 recent articles and the "From the archives" section, showing the 6 best rated articles

##Search results page
* Shows the result of the entered search
