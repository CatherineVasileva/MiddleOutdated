[System.Serializable]

public class User 
{
    public string name;
    public int score;

    public User()
    {

    }

    public User (string name, int score)
    {
        this.name = name;
        this.score = score;
    }
}
