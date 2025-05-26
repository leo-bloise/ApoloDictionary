# 📝 Apolo Dictionary
Apolo Dictionary is a simple and fast dictionary application that allows you to search for words and their meanings. It is built using C# and the System.CommandLine library for CLI arguments and options parsing. It also uses webscraping to fetch definitions from the web.

## 🌐 Current Web Scraping Sources
- [Dictionary Cambridge](https://dictionary.cambridge.org)

## How to Use

After installing the application, you can use it from the command line. Here are some examples:

```bash
$ ./ApoploDictionary.exe --help

Description:
  Apolo Dictionary - Your CLI dictionary

Usage:
  ApoloDictionary [command] [options]

Options:
  --version       Show version information
  -?, -h, --help  Show help and usage information

Commands:
  translate  Translate a word or phrase
```

Translate a word or phrase:

```bash
.\ApoloDictionary.exe translate --text Hello

Provider: Dictionary Cambridge

Text: Hello
Grammar Classification: exclamation, noun
CEFR: A1
Meaning: used when meeting or greeting someone

Example: Hello, Paul. I haven't seen you for ages.
Example: I know her vaguely - we've exchanged hellos a few times.
Example: say hello I just thought I'd call by and say hello.
Example: a big hello And a big hello (= welcome) to all the parents who've come to see the show.

--------------
Provider: Dictionary Cambridge

Text: Hello
Grammar Classification: exclamation, noun
CEFR: A1
Meaning: something that is said at the beginning of a phone conversation

Example: "Hello, I'd like some information about flights to the US, please."

--------------
Provider: Dictionary Cambridge

Text: Hello
Grammar Classification: exclamation, noun
CEFR: None
Meaning: something that is said to attract someone's attention

Example: The front door was open so she walked inside and called out, "Hello! Is there anybody in?"

--------------
Provider: Dictionary Cambridge

Text: Hello
Grammar Classification: exclamation, noun
CEFR: informal
Meaning: said to someone who has just said or done something stupid, especially something that shows they are not noticing what is happening

Example: She asked me if I'd just arrived and I was like "Hello, I've been here for an hour."

--------------
Provider: Dictionary Cambridge

Text: Hello
Grammar Classification: exclamation, noun
CEFR: old-fashioned
Meaning: an expression of surprise

Example: Hello, this is very strange - I know that man.

--------------
```