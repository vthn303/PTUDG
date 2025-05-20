using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using System.Linq; // for LINQ methods

public class FindMatches : MonoBehaviour
{
    public Board board;
    public List<GameObject> currentMatches = new List<GameObject>();
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        board = FindObjectOfType<Board>();

    }
    public void FindAllMatches()
    {
               StartCoroutine(FindAllMatchesCo());
    }

    private IEnumerator FindAllMatchesCo()
    {
        yield return new WaitForSeconds(0.2f);
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                GameObject currentDot = board.allDots[i, j];
                if (currentDot != null)
                {

                    {
                        if (i > 0 && i < board.width - 1) // check rows
                        {
                            GameObject leftDot = board.allDots[i - 1, j];
                            GameObject rightDot = board.allDots[i + 1, j];
                            if (leftDot != null && rightDot != null)
                            {
                                if (leftDot.tag == currentDot.tag && rightDot.tag == currentDot.tag)
                                {
                                    if (currentDot.GetComponent<Dot>().isRowBomb || leftDot.GetComponent<Dot>().isRowBomb|| rightDot.GetComponent<Dot>().isRowBomb)
                                    {
                                        currentMatches.Union(GetRowPieces(j));
                                    }
                                    if (currentDot.GetComponent<Dot>().isColumnBomb)
                                    {
                                        currentMatches.Union(GetColumnPieces(i));
                                    }

                                    if(leftDot.GetComponent<Dot>().isColumnBomb)
                                    {
                                        currentMatches.Union(GetColumnPieces(i-1));
                                    }

                                    if(rightDot.GetComponent<Dot>().isColumnBomb)
                                    {
                                        currentMatches.Union(GetColumnPieces(i+1));
                                    }

                                    if (!currentMatches.Contains(leftDot))
                                    {
                                        currentMatches.Add(leftDot);
                                    }
                                    leftDot.GetComponent<Dot>().isMatched = true;
                                    if (!currentMatches.Contains(rightDot))
                                    {
                                        currentMatches.Add(rightDot);
                                    }
                                    rightDot.GetComponent<Dot>().isMatched = true;
                                    if (!currentMatches.Contains(currentDot))
                                    {
                                        currentMatches.Add(currentDot);
                                    }
                                    currentDot.GetComponent<Dot>().isMatched = true;
                                }
                            }

                        }

                        if (j > 0 && j < board.height - 1) // check columns
                        {
                            GameObject upDot = board.allDots[i, j + 1];
                            GameObject downDot = board.allDots[i, j - 1];
                            if (upDot != null && downDot != null)
                            {
                                if (upDot.tag == currentDot.tag && downDot.tag == currentDot.tag)
                                {
                                    if (currentDot.GetComponent<Dot>().isColumnBomb || upDot.GetComponent<Dot>().isColumnBomb || downDot.GetComponent<Dot>().isColumnBomb)
                                    {
                                        currentMatches.Union(GetColumnPieces(i));
                                    }
                                    if(currentDot.GetComponent<Dot>().isRowBomb)
                                    {
                                        currentMatches.Union(GetRowPieces(j));
                                    }
                                    if (upDot.GetComponent<Dot>().isRowBomb)
                                    {
                                        currentMatches.Union(GetRowPieces(j + 1));
                                    }
                                    if (downDot.GetComponent<Dot>().isRowBomb)
                                    {
                                        currentMatches.Union(GetRowPieces(j - 1));
                                    }
                                    if (!currentMatches.Contains(upDot))
                                    {
                                        currentMatches.Add(upDot);
                                    }
                                    upDot.GetComponent<Dot>().isMatched = true;
                                    if (!currentMatches.Contains(downDot))
                                    {
                                        currentMatches.Add(downDot);
                                    }
                                    downDot.GetComponent<Dot>().isMatched = true;
                                    if (!currentMatches.Contains(currentDot))
                                    {
                                        currentMatches.Add(currentDot);
                                    }
                                    currentDot.GetComponent<Dot>().isMatched = true;
                                }
                            }

                        }

                    }
                }
            }
        }
    }

    public void MatchPiecesOfColor(string color)
    {
        for (int i = 0; i < board.width; i++)
        {
            for (int j = 0; j < board.height; j++)
            {
                //Check if that piece exists
                if (board.allDots[i, j] != null)
                {
                    //Check the tag on that dot
                    if (board.allDots[i, j].tag == color)
                    {
                        //Set that dot to be matched
                        board.allDots[i, j].GetComponent<Dot>().isMatched = true;
                    }
                }
            }
        }
    }


    List<GameObject> GetColumnPieces(int column)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int  i = 0;  i < board.height;  i++)
        {
            if (board.allDots[column, i] != null)
            {
                dots.Add(board.allDots[column, i]);
                board.allDots[column, i].GetComponent<Dot>().isMatched = true;
            }

        }
        return dots;
    }

    List<GameObject> GetRowPieces(int row)
    {
        List<GameObject> dots = new List<GameObject>();
        for (int i = 0; i < board.width; i++) 
        {
            if (board.allDots[row, i] != null)
            {
                dots.Add(board.allDots[i,row]);
                board.allDots[i, row].GetComponent<Dot>().isMatched = true;
            }

        }
        return dots;
    }

    public void CheckBombs()
    {
        // is move ?
        if(board.currentDot != null)
        {
            //piece they move matches ?
            if (board.currentDot.isMatched)
            {
                board.currentDot.isMatched = false;
                int typeOfBomb = Random.Range(0, 100);
                if(typeOfBomb < 50)
                {
                    //row bomb
                    board.currentDot.MakeRowBomb();
                }else if(typeOfBomb >= 50)
                {
                    // column bomb
                    board.currentDot.MakeColumnBomb();
                }

            }
            // other piece matches ?
            else if (board.currentDot.otherDot != null)
            {
                Dot otherDot = board.currentDot.otherDot.GetComponent<Dot>();
                if (otherDot.isMatched)
                {
                    otherDot.isMatched = false;
                    int typeOfBomb = Random.Range(0, 100);
                    if (typeOfBomb < 50)
                    {
                        //row bomb
                        otherDot.MakeRowBomb();
                    }
                    else if (typeOfBomb >= 50)
                    {
                        // column bomb
                        otherDot.MakeColumnBomb();
                    }

                }

            }
        }
    }



}
