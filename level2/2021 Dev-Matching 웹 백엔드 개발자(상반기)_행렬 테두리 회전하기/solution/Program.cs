using System;
using System.Collections.Generic;
using System.Linq;
public class Solution {
    public int[] solution(int rows, int columns, int[,] queries) {
        int[,] matrix = new int[rows + 1, columns + 1 ];//2차원 배열 생성(1기반 인덱스)
        List<int> answer = new List<int>();//결과를 저장할 리스트 -> 후에 배열로 변환


        for(int i=1; i<=rows; i++)// 2중 for문으로 행과 열을 채워준다.
        {
            for(int j=1; j<=columns; j++)
            {
                matrix[i, j] = (i-1) * columns + j;// 각 위치에 ((i-1) * columns + j)를 넣어준다.
            }
        }


        for(int i=0; i<queries.GetLength(0); i++)// 각 쿼리에 대해 회전을 수행한다.
        {
            int x1 = queries[i, 0];// 첫 행 좌표
            int y1 = queries[i, 1];// 첫 열 좌표
            int x2 = queries[i, 2];// 두 번째 행 좌표
            int y2 = queries[i, 3];// 두 번째 열 좌표

            int temp = matrix[x1, y1];//임시 변수에 x1,y1의 값을 저장
            int min = temp;//최소값을 저장할 변수에 temp값을 저장

            for(int j = x1; j < x2; j++) // 왼쪽 열 숫자들을 위로 이동
            {
                matrix[j, y1] = matrix[j+1, y1];
                min = Math.Min(min, matrix[j, y1]);// 최솟값 저장
            }

            for(int j = y1; j < y2; j++)//아래 행 숫자들을 왼쪽으로 이동
            {
                matrix[x2, j] = matrix[ x2, j+1];
                min  = Math.Min(min, matrix[x2, j]);// 최솟값 저장
            }

            for(int j=x2; j>x1; j--)//오른쪽 열 숫자들을 아래로 이동
            {
                matrix[j, y2] = matrix[j-1, y2];
                min = Math.Min(min, matrix[j, y2]);// 최솟값 저장   
            }

            for(int j = y2; j> y1; j--)// 위쪽 행의 숫자들을 오른쪽으로 이동
            {
                matrix[x1, j] = matrix[x1, j-1];
                min = Math.Min(min, matrix[x1, j]);// 최솟값 저장
            }

            // 원래 x1y1에 있던 값은 x1, y1+1로 이동(우측으로 한 칸 이동)
            matrix[x1, y1+1] = temp;
            answer.Add(min);//최솟값을 리스트에 추가
        }
        return answer.ToArray();

    }


    public static void Main(string[] args)
    {
        Solution solution = new Solution();
        int rows = 3;
        int columns = 3;
        int[,] queries = new int[,] { {1,1,2,2}, {1,2,2,3}, {2,1,3,2}, {2,2,3,3} };
        int[] result = solution.solution(rows, columns, queries);

        System.Console.WriteLine($"[{string.Join(", ",result)}]");
    }

    /*
    rows * columns크기인 행렬에 1부터 rows*columns까지의 숫자가 한 줄씩 순서대로 적혀 있음
    이 행렬에서 직사각형 모양의 범위를 여러 번 선택해 테두리 부분에 있는 숫자들을 시계방향으로 회전시켜야 함
    각 회전은 (x1,y1,x2,y2)인 정수 4개 => x1행 y1열 부터 x2행 y2열까지의 영역에 해당하는 직사각형에서 테두리에 있는 숫자들을 한 칸씩 시계방향 회전
    
    행렬의 세로 길이(행 개수) row, 가로 길이(열 개수) columns, 회전들의 목록 queries => 각 회전들을 배열에 적용한 뒤 그 회전에 의해 위치가 바뀐 숫자들 중 가장 작은 숫자들을
    순서대로 배열에 담아 리턴
    rows는 2 이상 100 이하
    coumns는 2 이상 100 이하
    아무 회전도 하지 않았을 때 i행 j열의 숫자는 ((i-1) * columns + j)
    queries의 각 행은 x1,y1,x2,y2

    ==> 6행 6열 크기 행렬에서 [2,2,5,4]를 회전시키면 회전하는 테두리는 [2행2열, 2행3열, 2행4열, 3행2열, 3행4열, 4행2열, 4행4열, 5행2열, 5행3열, 5행4열]
                                                            가운데의 [3행3열, 4행3열]은 회전하지 않음

    ==> 3행 3열 크기 행렬에서 [1,1,2,2]를 회전시키면 회전하는 테두리는 [1행1열, 1행2열, 2행1열, 2행2열]

    ==> 아무 회전도 하지 않았을 때 i행 j열의 숫자는 ((i-1) * columns + j) 이니까, rows와 colums를 받으면 2차원 배열을 생성해서 queries에 적힌 [x1,y1,x2,y2]를 받아서,
    테두리를 만들어야 함 => x1,y1과 x2,y2는 서로 대각선 상에 위치한 점-> x1,y1이 x2,y2보다 왼쪽 위에 위치할 것이니까,  
    테두리의 꼭짓점은 { (x1,y1), (x1, y2), (x2,y1), (x2,y2) }이렇게 4개가 된다.

    왼쪽 열의 숫자들을 위로 이동 -> 아래 행의 숫자들을 왼쪽으로 이동 -> 오른쪽 열의 숫자들을 아래로 이동 -> 위쪽 행의 숫자들을 오른쪽으로 이동


    1. 초기 행렬 rows * columns 생성 
    2. 각 위치에 ((i-1) * columns + j)를 넣어준다. (반복)
    3. 각 쿼리 (x1,y1,x2,y2)에 대해 테두리 부분 숫자들을 orderby ASC로 정렬
    4. 테두리 숫자들을 시계방향으로 한 칸씩 이동키고 이 과정에서 가장 작은 숫자들을 추적
    5. 각 쿼리 처리 후, 회전된 숫자들 중 가장 작은 숫자를 result에 저장
    6. result를 반환
    */
}