# Build Your Own Compression Tool
This compression tool is built following [this challenge][link]. The tool uses [Huffman Encoding][huffman] to compress files to a smaller size, and is capable of decoding the file using the same encoding method.
## Usage
The tool can be run by using this command:
```
CompressionTool [command] <input file> <output file>
```
* command:
  - `encode`: Encodes the `input file` with huffman encoding and create a compressed `output file`
  - `decode`: Decodes the `input file` if it uses huffman encoding, and creates a decompressed `output file`
  
Beware that this tool will override the contents of the `output file` if the file exists.

[link]:https://codingchallenges.fyi/challenges/challenge-huffman
[huffman]: https://opendsa-server.cs.vt.edu/ODSA/Books/CS3/html/Huffman.html