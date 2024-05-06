# Cat
A copy of the Unix Command line tool cat. It prints out the input files, if more than one files is input, it concatenate the contents. This is built following this [challenge][link].

## Usage
```
Cat <options> [files]
```
- `-n`: Include line number in front of each line 
- `-b`: Include line number in front of each non blank line

`files`: The input file or files to concatenate. If the file is empty or '-', the program reads from `stdin` instead.


[link]:https://codingchallenges.fyi/challenges/challenge-cat/