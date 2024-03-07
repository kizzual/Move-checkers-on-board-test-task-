using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardInitializer : MonoBehaviour
{
    [SerializeField] private GameObject cellPrefab;
    [SerializeField] private Checker checkerPrefab_red;
    [SerializeField] private Checker checkerPrefab_beige;

    private readonly int m_coulmnCount = 8;
    private readonly int m_rawnCount = 8;
    public List<Cell> m_cells = new List<Cell>();

    void Start()
    {
        ChangeColorCells();
        StartCoroutine(CheckerCpawnDelay());
    }
    
    private void ChangeColorCells()
    {
        for (int i = 0; i < m_coulmnCount * m_rawnCount; i++)
        {
            m_cells[i].name = "Cell_" + i.ToString();

            int row = i / m_rawnCount;
            int col = i % m_coulmnCount;

            if ((row + col) % 2 == 0)
                m_cells[i].SetNewColor(Color.white);
            else
                m_cells[i].SetNewColor(Color.black);
        }
    }
    private void ChechekrsSpawn()
    {
        for (int i = 0; i < 16; i++)
        {
            Checker checker = Instantiate(checkerPrefab_red, transform.GetChild(2));
            checker.SetNewPosition(m_cells[i].GetPosition());
        }
        for (int i = 48; i < 64; i++)
        {
            Checker checker = Instantiate(checkerPrefab_beige, transform.GetChild(2));
            checker.SetNewPosition(m_cells[i].GetPosition());
        }
    }
    IEnumerator CheckerCpawnDelay()
    {
        yield return new WaitForSeconds(0.01f);
        ChechekrsSpawn();
    }
}
