using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEngine.Rendering.DebugUI.Table;

[RequireComponent(typeof(SceneTransfer))]
public class BallCollector : MonoBehaviour
{
    [SerializeField]
    private Ball[] _zone;
    [SerializeField]
    private int _size;
    [SerializeField]
    private SceneTransfer _transfer;

    private void Awake()
    {
        _zone = new Ball[9];
    }

    private void Start()
    {
        GameManager.Score = 0;
    }

    public void AddBall(int zoneIndex, Ball ball)
    {
        for(int i = 0; i < _size; i++)
        {
            if (Score(i,zoneIndex) == 0)
            {
                _zone[i + (zoneIndex * _size)] = ball;
                Debug.Log(_zone[i + (zoneIndex * _size)].gameObject);
                Check();
                break;
            }
        }
    }

    private void Check()
    {
        bool c = false, r = false, md = false, ad = false;
        int col = -1, row = -1;

        for (int i = 0; i < _size; i++)
        {
            if(CheckColumn(i))
            {
                c = true;
                col = i;
                break;
            }
        }

        for (int i = 0; i < _size; i++)
        {
            if (CheckRow(i))
            {
                r = true;
                row = i;
                break;
            }
        }

        md = CheckMainDiagonal();
        ad = CheckAntiDiagonal();

        if (!c && !r && !md && !ad)
        {
            CheckEndGame();
            return;
        }

        if (c) { DestroyColumn(col);}
        if (r) { DestroyRow(row); }
        if (md) { DestroyMainDiagonal(); }
        if (ad) { DestroyAntiDiagonal(); }

        if (r || md || ad) { MakeShift(); }
    }

    private void CheckEndGame()
    {
        foreach(Ball b in _zone)
        {
            if (b == null) return;
        }

        StartCoroutine(Load());
    }

    private IEnumerator Load()
    {
        yield return new WaitForSeconds(1);

        _transfer.Load(2);
    }

    private void MakeShift()
    {
        for (int i = 0; i < _size - 1; i++)
        {
            for(int j = 0; j < _size; j++)
            {
                if (_zone[i + (j * _size)] == null)
                {
                    _zone[i + (j * _size)] = _zone[i + (j * _size) + 1];
                    _zone[i + (j * _size) + 1] = null;
                }
            }
        }

        Check();
    }

    private void DestroyColumn(int col)
    {
        for (int i = 0; i < _size; i++)
        {
            _zone[i + (col * _size)].MakeABoom();
            _zone[i + (col * _size)] = null;
        }
    }

    private void DestroyRow(int row)
    {
        for (int i = 0; i < _size; i++)
        {
            if (_zone[row + (i * _size)].gameObject == null) continue;
            _zone[row + (i * _size)].MakeABoom();
            _zone[row + (i * _size)] = null;
        }
    }

    private void DestroyMainDiagonal()
    { 
        for (int i = 0; i < _size; i++)
        {
            if (_zone[i + (i * _size)].gameObject == null) continue;
            _zone[i + (i * _size)].MakeABoom();
            _zone[i + (i * _size)] = null;
        }
    }

    private void DestroyAntiDiagonal()
    { 
        for(int i = 0; i < _size; i++)
        {
            if (_zone[i + ((_size - i - 1) * _size)].gameObject == null) continue;
            _zone[i + ((_size - i - 1) * _size)].MakeABoom();
            _zone[i + ((_size - i - 1) * _size)] = null; 
        }
    }

    private bool CheckColumn(int col)
    {
        int val = Score(0,col);
        if (val == 0) return false;

        for (int i = 1; i < _size; i++)
        {
            if (Score(i, col) == 0 || Score(i, col) != val) return false;
        }

        GameManager.Score += val * _size;

        Debug.Log(col + " column is one color score: " + GameManager.Score);

        return true;
    }

    private bool CheckRow(int row)
    {
        int val = Score(row,0);
        if (val == 0) return false;

        for(int i  = 1; i < _size; i++)
        {
            if (Score(row,i) == 0 || Score(row,i) != val) return false;
        }

        GameManager.Score += val * _size;

        Debug.Log(row + " row is one color score: " + GameManager.Score);

        return true;
    }

    private bool CheckMainDiagonal()
    {
        int val = Score(0,0);
        if (val == 0) return false;

        for(int i = 1; i < _size; i++)
        {
            if (Score(i, i) == 0 || Score(i, i) != val) return false;
        }

        GameManager.Score += val * _size;

        Debug.Log("Main diagonal is one color score: " + GameManager.Score);        

        return true;
    }

    private bool CheckAntiDiagonal()
    {
        int val = Score(0, _size - 1);
        if (val == 0) return false;

        for (int i = 1; i < _size; i++)
        {
            if (Score(i, _size - i - 1) == 0 || Score(i, _size - i - 1) != val) return false;
        }

        GameManager.Score += val * _size;

        Debug.Log("Anti diagonal is one color score: " + GameManager.Score);  

        return true;
    }

    private void PrintZone()
    {
        string s = "";
        foreach(Ball b in _zone)
        {
            s += (b == null ? 0 : b.Score);
        }
        Debug.Log(s);
    }

    private int Score(int row, int col) => _zone[row + (col * _size)] == null? 0 : _zone[row + (col * _size)].Score;

}
