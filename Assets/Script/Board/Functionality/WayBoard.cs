using System.Collections.Generic;
using UnityEngine;

namespace Board
{
    public class WayBoard : IWayBoard
    {
        private int _largeOfTheListRow = 4;
        private int _largeOfTheListColumn = 4;
        private int[] _dataRandomValueInTheWay;

        List<List<int>> way = new List<List<int>>();
        List<Vector2> firstWayTerminated = new List<Vector2>();
        List<Vector2> secondWayTerminated = new List<Vector2>();
        List<Vector2> cornerWayFirstList = new List<Vector2>();
        List<Vector2> cornerWaySecondList = new List<Vector2>();
        List<Vector2> realWayToPlayer = new List<Vector2>();

        private int m_initialPosition = 0;
        private int m_lastPosition = 0;
        private int m_newChangePositionIndex = 0;

        private int m_initialPositionDataInTheFirstWay = 0;
        private int m_lastPositionDataInTheFirstWay = 0;
        private int m_initialPositionDataInTheSecondWay = 0;
        private int m_lastPositionDataInTheSecondWay = 0;

        private int largeColumnSubtractValue { get { return _largeOfTheListColumn - 1; } }

        public WayBoard(int largeOfTheListRow, int largeOfTheListColumn, int[] dataRandomValueInTheWay)
        {
            _largeOfTheListRow = largeOfTheListRow;
            _largeOfTheListColumn = largeOfTheListColumn;
            _dataRandomValueInTheWay = dataRandomValueInTheWay;
        }

        public List<List<int>> GetCalculateNewGameWay()
        {
            AddElementsInTheControlList(0, 0);

            CalculateWayWithKeys(0);
            UpdateDataForConnectWays(0);
            CalculateWayContinueReturn();
            UpdateDataForConnectWays(1);
            CalculateWayCornersToConnect();

            return way;
        }

        public List<Vector2> GetTheRealWayMovePlayer() => realWayToPlayer;

        private void CalculateWayCornersToConnect()
        {

            //add the first and the last column
            for (int i = 0; i < _largeOfTheListRow * 2 + 1; i++)
            {
                way[i].Insert(0, 0);
                way[i].Insert(largeColumnSubtractValue, 0);
            }

            //add corners
            way[m_initialPositionDataInTheFirstWay][0] = 1;
            cornerWayFirstList.Add(new Vector2(m_initialPositionDataInTheFirstWay, 0));
            way[m_lastPositionDataInTheFirstWay][largeColumnSubtractValue] = 1;
            cornerWaySecondList.Add(new Vector2(m_lastPositionDataInTheFirstWay, largeColumnSubtractValue));

            for (int i = m_initialPositionDataInTheFirstWay + 1; i <= m_initialPositionDataInTheSecondWay; i++)
            {
                way[i][0] = GetNewRandomKeyInThePlace();
                cornerWayFirstList.Add(new Vector2(i, 0));
            }

            for (int i = m_lastPositionDataInTheFirstWay + 1; i <= m_lastPositionDataInTheSecondWay; i++)
            {
                way[i][largeColumnSubtractValue] = GetNewRandomKeyInThePlace();
                cornerWaySecondList.Add(new Vector2(i, largeColumnSubtractValue));
            }

            CalculateCompleteWayToPlayer();

        }

        private void CalculateCompleteWayToPlayer()
        {
            //complete the way  key
            secondWayTerminated.Reverse();
            cornerWayFirstList.Reverse();

            //add elements in the new list
            for (int i = 0; i < firstWayTerminated.Count; i++)
            {
                realWayToPlayer.Add(firstWayTerminated[i]);
            }

            for (int i = 0; i < cornerWaySecondList.Count; i++)
            {
                realWayToPlayer.Add(cornerWaySecondList[i]);
            }

            for (int i = 0; i < secondWayTerminated.Count; i++)
            {
                realWayToPlayer.Add(secondWayTerminated[i]);
            }

            for (int i = 0; i < cornerWayFirstList.Count; i++)
            {
                realWayToPlayer.Add(cornerWayFirstList[i]);
            }

            /*print(realWayToPlayer[0]);
            print(realWayToPlayer[realWayToPlayer.Count - 1]);*/
        }

        private void CalculateWayInitialParameters(int valueToAugment)
        {
            //intial position
            m_initialPosition = GetNewRandomInitialPosition() + valueToAugment;
            way[m_initialPosition][0] = 1;
            ChangeDataKeyInThePrincipalWayVector(valueToAugment, m_initialPosition, 0);

            m_newChangePositionIndex = m_initialPosition;
            m_lastPosition = m_newChangePositionIndex;
        }

        private void CalculateWayWithKeys(int valueToAugment)
        {
            CalculateWayInitialParameters(valueToAugment);

            //second position
            for (int j = 1; j < _largeOfTheListColumn - 1; j++)
            {
                //new position, and save last position
                if (m_lastPosition == m_newChangePositionIndex && j < _largeOfTheListColumn - 3)
                {
                    m_newChangePositionIndex = m_lastPosition - Random.Range(-1, 2); //2 exclusive, the real range is (-1,0,1)
                }
                else
                {
                    m_lastPosition = m_newChangePositionIndex;
                }

                //control range
                m_newChangePositionIndex = Mathf.Min(m_newChangePositionIndex, _largeOfTheListRow - 1 + valueToAugment);
                m_newChangePositionIndex = Mathf.Max(m_newChangePositionIndex, valueToAugment);

                //change values in list
                if (m_newChangePositionIndex != m_lastPosition)
                {
                    ChangeDataKeyInThePrincipalWayVector(valueToAugment, m_lastPosition, j);
                }

                ChangeDataKeyInThePrincipalWayVector(valueToAugment, m_newChangePositionIndex, j);
            }
        }

        private void ChangeDataKeyInThePrincipalWayVector(int valueToAugment, int firstPosition, int secondPosition)
        {
            int newRandomKey = GetNewRandomKeyInThePlace();
            way[firstPosition][secondPosition] = newRandomKey;
            UpdateListWayTerminated(valueToAugment, firstPosition, secondPosition + 1);
        }

        private void UpdateListWayTerminated(int valueToAugment, int firstPosition, int secondPosition) {
            if (secondPosition >= _largeOfTheListColumn - 1) return;
            if (valueToAugment == 0)
                firstWayTerminated.Add(new Vector2(firstPosition, secondPosition));
            else
                secondWayTerminated.Add(new Vector2(firstPosition, secondPosition));
        }

        private void UpdateDataForConnectWays(int valueData)
        {
            //save data
            if (valueData == 0)
            {
                m_initialPositionDataInTheFirstWay = m_initialPosition;
                m_lastPositionDataInTheFirstWay = m_lastPosition;
            }
            else
            {
                m_initialPositionDataInTheSecondWay = m_initialPosition;
                m_lastPositionDataInTheSecondWay = m_lastPosition;
            }
        }

        private void CalculateWayContinueReturn() {
            AddElementsInTheControlList(_largeOfTheListRow, _largeOfTheListRow + 1);
            CalculateWayWithKeys(_largeOfTheListRow + 1);
        }

        private void AddElementsInTheControlList(int valueToStartCount, int valueToFinishCount)
        {
            for (int i = 0 + valueToStartCount; i < _largeOfTheListRow + valueToFinishCount; i++)
            {
                way.Add(new List<int>());
                for (int j = 0; j < _largeOfTheListColumn; j++)
                {
                    way[i].Add(0);
                }
            }
        }

        private int GetNewRandomInitialPosition() => Random.Range(0, _largeOfTheListRow);

        private int GetNewRandomKeyInThePlace() => _dataRandomValueInTheWay[Random.Range(0, _dataRandomValueInTheWay.Length)];

        private int GetTheMaxTwoNumbers(int firstValue, int secondValue) => Mathf.Max(firstValue, secondValue);

        private int GetTheMinTwoNumbers(int firstValue, int secondValue) => Mathf.Min(firstValue, secondValue);
    }
}