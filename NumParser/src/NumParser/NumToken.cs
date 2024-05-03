
enum TokenType
{
  Number,
  Multiplier,
};

struct NumToken
{
  public TokenType type;
  public long value;

  public NumToken(TokenType type, long value)
  {
    this.type = type;
    this.value = value;
  }
}