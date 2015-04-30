using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Assignment1
{
    class NewCrozzle
    {
        private int cols, rows;
        char EMPTYCHAR = ' ';

        int FIT_ATTEMPTS = 1;

        public List<WordArray> activeWordList; //keeps array of words actually placed in board
        int acrossCount = 0;
        int downCount = 0;
        public GridArray[][] crozzle_board;
        public List<CoordinateList> coordList;
        public WordArray[] word_list;

        public NewCrozzle(int c, int r, string[] words)
        {
            this.cols = c;
            this.rows = r;
            this.activeWordList = new List<WordArray>();
            this.coordList = new List<CoordinateList>();
            this.word_list = new WordArray[words.Length];

            int n = 0;
            foreach (string s in words)
            {
                word_list[n] = new WordArray();
                word_list[n].x = -1;
                word_list[n].y = -1;
                word_list[n].number = -1;
                word_list[n].vertical = false;
                word_list[n].word = s;
                n++;
            }

            this.crozzle_board = new GridArray[rows][];
            for (int i = 0; i < rows; i++)
            {
                crozzle_board[i] = new GridArray[cols];

                for (int j = 0; j < cols; j++)
                {
                    crozzle_board[i][j] = new GridArray();
                }
            }

            for (int i = 0; i < rows; i++)
            {
                for (int j = 0; j < cols; j++)
                {
                    crozzle_board[i][j].targetChar = EMPTYCHAR; //target character, hidden
                    crozzle_board[i][j].indexDisplay = ' '; //used to display index number of word start
                    crozzle_board[i][j].value = '-'; //actual current letter shown on board
                }
            }
        }

        private void suggestCoords(string word) { //search for potential cross placement locations
            char c = ' ';
            for (int i = 0; i < word.Length; i++) { //cycle through each character of the word
                for (int x = 0; x < rows; x++) {
                    for (int y = 0; y < cols; y++) {
                        c = word[i];
                        if (crozzle_board[x][y].targetChar == c) { //check for letter match in cell
                            if (x - i + 1> 0 && x - i + word.Length-1 < rows) { //would fit vertically?
                                CoordinateList tempCoordList = new CoordinateList();
                                tempCoordList.x = x - i;
                                tempCoordList.y = y;
                                tempCoordList.score = 0;
                                tempCoordList.vertical = true;
                                coordList.Add(tempCoordList);
                            }

                            if (y - i + 1 > 0 && y - i + word.Length-1 < cols) { //would fit horizontally?
                                CoordinateList tempCoordList = new CoordinateList();
                                tempCoordList.x = x;
                                tempCoordList.y = y - i;
                                tempCoordList.score = 0;
                                tempCoordList.vertical = false;
                                coordList.Add(tempCoordList);
                            }
                        }
                    }
                }
            }
        }
        
        private int checkFitScore(string word, int x, int y, bool vertical) {

            int fitScore = 1; //default is 1, 2+ has crosses, 0 is invalid due to collision

            if (vertical) { 
                //vertical checking
                for (int i = 0; i < word.Length; i++) {
                    if (i == 0 && x > 0) 
                    { 
                        //check for empty space preceeding first character of word if not on edge
                        if (crozzle_board[x - 1][y].targetChar != EMPTYCHAR)
                        { 
                            //adjacent letter collision
                            fitScore = 0;
                            break;
                        }
                    }
                    else if (i == word.Length-1 && x + i < rows - 1)
                    { 
                        //check for empty space after last character of word if not on edge
                        if (crozzle_board[x + i + 1][y].targetChar != EMPTYCHAR)
                        { 
                            //adjacent letter collision
                            fitScore = 0;
                            break;
                        }
                    }
                    if (x + i < rows)
                    {
                        if (crozzle_board[x + i][y].targetChar == word[i])
                        { 
                            //letter match - aka cross point
                            fitScore += 1;
                        }
                        else if (crozzle_board[x + i][y].targetChar != EMPTYCHAR)
                        { 
                            //letter doesn't match and it isn't empty so there is a collision
                            fitScore = 0;
                            break;
                        } 
                        else 
                        { 
                            //verify that there aren't letters on either side of placement if it isn't a crosspoint
                            if (y < cols - 1) 
                            { 
                                //check right side if it isn't on the edge
                                if (crozzle_board[x + i][y + 1].targetChar != EMPTYCHAR)
                                { 
                                    //adjacent letter collision
                                    fitScore = 0;
                                    break;
                                }
                            }
                            if (y > 0) 
                            { 
                                //check left side if it isn't on the edge
                                if (crozzle_board[x + i][y - 1].targetChar != EMPTYCHAR)
                                { 
                                    //adjacent letter collision
                                    fitScore = 0;
                                    break;
                                }
                            }
                        }
                    }

                }

            } 
            else 
            { 
                //horizontal checking
                for (int i = 0; i < word.Length; i++) 
                {
                    if (i == 0 && y > 0) 
                    { 
                        //check for empty space preceeding first character of word if not on edge
                        if (crozzle_board[x][y - 1].targetChar != EMPTYCHAR) 
                        { 
                            //adjacent letter collision
                            fitScore = 0;
                            break;
                        }
                    }
                    else if (i == word.Length - 1 && y + i < cols - 1) 
                    { 
                        //check for empty space after last character of word if not on edge
                        if (crozzle_board[x][y + i + 1].targetChar != EMPTYCHAR) 
                        { 
                            //adjacent letter collision
                            fitScore = 0;
                            break;
                        }
                    }
                    if (y + i < cols) 
                    {
                        if (crozzle_board[x][y + i].targetChar == word[i]) 
                        { 
                            //letter match - aka cross point
                            fitScore += 1;
                        }
                        else if (crozzle_board[x][y + i].targetChar != EMPTYCHAR) 
                        {
                            //letter doesn't match and it isn't empty so there is a collision
                            fitScore = 0;
                            break;
                        } 
                        else 
                        { 
                            //verify that there aren't letters on either side of placement if it isn't a crosspoint
                            if (x < rows) 
                            {
                                //check top side if it isn't on the edge
                                if (crozzle_board[x + 1][y + i].targetChar != EMPTYCHAR) 
                                {
                                    //adjacent letter collision
                                    fitScore = 0;
                                    break;
                                }
                            }
                            if (x > 0) 
                            {
                                //check bottom side if it isn't on the edge
                                if (crozzle_board[x - 1][y + i].targetChar != EMPTYCHAR) 
                                {
                                    //adjacent letter collision
                                    fitScore = 0;
                                    break;
                                }
                            }
                        }
                    }

                }
            }

            return fitScore;
        }

        private void placeWord(string word, int x, int y, bool vertical) { //places a new active word on the board

            var wordPlaced = false;

            if (vertical) {
                if (word.Length + x < rows) {
                    for (int i = 0; i < word.Length; i++) {
                        crozzle_board[x + i][y].targetChar = word[i];
                    }
                    wordPlaced = true;
                }
            } else {
                if (word.Length + y < cols) {
                    for (int i = 0; i < word.Length; i++) {
                        crozzle_board[x][y + i].targetChar = word[i];
                    }
                    wordPlaced = true;
                }
            }

            if (wordPlaced) {
                //var currentIndex = activeWordList.Count;
                WordArray tempActiveWordList = new WordArray();
                tempActiveWordList.word = word;
                tempActiveWordList.x = x;
                tempActiveWordList.y = y;
                tempActiveWordList.vertical = vertical;

                if (tempActiveWordList.vertical)
                {
                    downCount++;
                    tempActiveWordList.number = downCount;
                }
                else
                {
                    acrossCount++;
                    tempActiveWordList.number = acrossCount;
                }
                activeWordList.Add(tempActiveWordList);
            }
        }

        private bool isActiveWord(string word) {
            if (activeWordList.Count > 0) {
                for (int w = 0; w < activeWordList.Count; w++)
                {
                    if (string.Equals(word, activeWordList[w].word)) {
                        //console.log(word + ' in activeWordList');
                        return true;
                    }
                }
            }
            return false;
        }
        
        public string[][] displayGrid() {

            string rowStr = "";
            string[][] result;
            result = new string [rows][];
            for (int x = 0; x < rows; x++)
            {
                result[x] = new string [cols];
                for (int y = 0; y < cols; y++) 
                {
                    if (crozzle_board[x][y].targetChar != ' ') result[x][y] = crozzle_board[x][y].targetChar.ToString();
                    else result[x][y] = "";
                    rowStr += " " + crozzle_board[x][y].targetChar + " ";
                }
                rowStr += "\n";

            }
            //Console.WriteLine(rowStr);
            //Console.WriteLine("across " + acrossCount);
            //Console.WriteLine("down " + downCount);
            
            return result;
        }

        public void generateBoard(int startX, int startY, bool isHorizontal)
        {
            int bestScoreIndex = 0;
            int topScore = 0;
            int fitScore = 0;
            //int startTime;

            //manually place the longest word horizontally at 0,0
            placeWord(word_list[0].word, startX, startY, isHorizontal);

            //attempt to fill the rest of the board 
            for (int iy = 0; iy < FIT_ATTEMPTS; iy++)
            { 
                //usually 2 times is enough for max fill potential
                for (int ix = 1; ix < word_list.Length; ix++)
                {
                    if (!isActiveWord(word_list[ix].word))
                    { 
                        //only add if not already in the active word list
                        topScore = 0;
                        bestScoreIndex = 0;

                        suggestCoords(word_list[ix].word); //fills coordList and coordCount
                        coordList = ShuffleArray(coordList); //adds some randomization

                        if (coordList.Count > 0)
                        {
                            for (int c = 0; c < coordList.Count; c++)
                            { 
                                //get the best fit score from the list of possible valid coordinates
                                fitScore = checkFitScore(word_list[ix].word, coordList[c].x, coordList[c].y, coordList[c].vertical);
                                if (fitScore > topScore)
                                {
                                    topScore = fitScore;
                                    bestScoreIndex = c;
                                }
                            }
                        }

                        if (topScore > 1)
                        { 
                            //only place a word if it has a fitscore of 2 or higher
                            placeWord(word_list[ix].word, coordList[bestScoreIndex].x, coordList[bestScoreIndex].y, coordList[bestScoreIndex].vertical);
                        }
                    }
                }
            }
        }

        private List<CoordinateList> ShuffleArray(List<CoordinateList> coordList)
        {
            var rand = new Random();
            for (int i = coordList.Count - 1; i > 0; i--)
            {
                int n = rand.Next(i + 1);
                CoordinateList temp = coordList[i];
                coordList[i] = coordList[n];
                coordList[n] = temp;
            }
            return coordList;
        }

        public class GridArray
        {
            public char targetChar;
            public char indexDisplay;
            public char value;
        }

        public class CoordinateList
        {
            public int x;
            public int y;
            public int score;
            public bool vertical;
            public char value;
        }

        public class WordArray
        {
            public int x;
            public int y;
            public string word;
            public bool vertical;
            public int number;
        }
    }
}
