using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject square;
    public GameObject W;
    public GameObject I;
    public GameObject L;
    public GameObject R;
    public GameObject S;
    public GameObject Z;

    private Tetramino curr_tetramino;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spawnTetramino();
    }

    void Update()
    {
        // wait for the curr_tetramino to deactivate
        if (!curr_tetramino.enabled) spawnTetramino();
    }

    void spawnTetramino()
    {
        // spawn a random tetramino
        switch (Random.Range(0, 7))
        {
            case 0:
                curr_tetramino = Instantiate(square, transform.position, transform.rotation).GetComponent<Tetramino>();
                break;
            case 1:
                curr_tetramino = Instantiate(W, transform.position, transform.rotation).GetComponent<Tetramino>();
                break;
            case 2:
                curr_tetramino = Instantiate(I, transform.position, transform.rotation).GetComponent<Tetramino>();
                break;
            case 3:
                curr_tetramino = Instantiate(L, transform.position, transform.rotation).GetComponent<Tetramino>();
                break;
            case 4:
                curr_tetramino = Instantiate(R, transform.position, transform.rotation).GetComponent<Tetramino>();
                break;
            case 5:
                curr_tetramino = Instantiate(S, transform.position, transform.rotation).GetComponent<Tetramino>();
                break;
            case 6:
                curr_tetramino = Instantiate(Z, transform.position, transform.rotation).GetComponent<Tetramino>();
                break;
        }
    }
}
