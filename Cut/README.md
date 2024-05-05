# Cut
This is a cut tool built following [this challenge][link]. The tool cuts out the selected portions from each line in a file.

## Usage
```
Cut <options> [file]
```
`options`:
- `-f`: The fields to cut out from each line. The format for the option is either `-f "1 2 3"` or `-f 1,2,3`
- `-d`: The delimiter character, the default is `'\t'` (tabulation character)

`file`: The file to read from. If left blank of '-' the program reads from standard input.

[link]:https://codingchallenges.fyi/challenges/challenge-cut