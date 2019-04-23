# DadJokeConsole

This is a .NET console application that will hit the https://icanhazdadjoke.com/ API.

It gives you a few options:

1. Display 30 dad jokes (search with no search term)
2. Dispay up to 30 dad jokes containing a specified search term
3. Display a random dad joke every 10 seconds

*BTW:* In the current implementation a valid search term is a single word that
can contain 1 or more alphanumeric characters and an apostrophe and will be
case-insensitive. If an invalid search term is provided, the application will 
clamp the search term down to an empty string and the first 30 jokes will be
returned.

This application was written on Debian Stretch Linux using the OpenSource .NET Core
framework.
