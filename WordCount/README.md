# WordCount (wc)
A copy of the UNIX command line tool wc. Based on [this challenge][link1].

## Usage
This tool can be run by using this command
```
WordCount <options> [file]
```
Options:
- `-c`: Outputs the byte count of the file
- `-m`: Outputs the character count of the file
- `-l`: Outputs line break characters(\n) in the file
- `-w`: Outputs the word count of the file. A word is defined as a string of characters delimited by spaces, tabs, or newline characters.

If no options are given, the tool outputs line count, word count, and byte count at the same time.

`file`: The file to be read from. If no file is provided or file is `-`, the program will read from `stdin`. All of these command are considered valid:
```
WordCount test.txt

WordCount < test.txt

WordCount - < test.txt
```
Reading from `stdin` currently only supports UTF8 encoding.

[link1]:https://codingchallenges.fyi/challenges/challenge-wc
