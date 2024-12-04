using System;
using System.Collections.Generic;
using System.Linq;
public class Solution {
    public int solution(int n, long l, long r) {
        return Calc(n, l, r);
        //호출순서 : Calc에서 비트열 계산 -> for문에서 [l, r]구간을 반복하며 1을 구한다. -> CountOne에서 1 찾음 -> result++
    }

    private int Calc(int n, long l, long r)//5의 승수만큼 커지는 비트열을 계산하는 함수
    {
        if(n==0) {return (l <=1 && 1 <=r) ? 1 : 0;}//0번째 칸토어 비트열 처리. 제한사항이 1<=l,r이므로 반대조건을 붙여 1이 출력됨
        
        int result = 0;// 결과 저장
        
        //비트열은 5의 승수만큼 커지니까, Pow로 계산한 값을 long변수에 넣음. int면 오류남
        long previousLength =(long)Math.Pow(5, n-1);

        for(long i = l; i <=r; i++)// 폐구간 l ~ r까지의 1을 센다.
        {
            result+= CountOne(n, i);// 각 구간 마다 n번째 폐구간에서의 1을 세야 함.
        }
     return result;
    }

    private int CountOne(int n, long position)// 비트열에서 1의 개수를 세는 함수
    {
        if(n==0) {return position ==1 ? 1 : 0;}//n이 0일 때(0번째) 위치가 1번째이면 1을 반환. 초기 비트열은 1이니까 1번 위치에만 1이 존재
        long previousLength = (long)Math.Pow(5, n-1);//이전 단계의 길이를 계산해야 함.
        
        //5의 승수 개 만큼 구간이 늘어날테니, 각 구간 별로 위치를 지정해서 탐색한다.(재귀사용)

        if(position <= previousLength){return CountOne(n-1, position);}//첫번째 비트 구간
        else if(position <= previousLength*2){return CountOne(n-1, position - previousLength);}//두번째 비트 구간
        else if (position <=previousLength*3){return 0;}// 11011이므로 가운데 부분은 0만 반환
        else if(position <=previousLength*4){return CountOne(n-1, position-previousLength*3);}//네번째 비트 구간
        else if(position <=previousLength*5) {return CountOne(n-1, position-previousLength*4);}//다섯번째 비트 구간
        /*
            만약 n=2이면 비트열은  11011 11011 00000 11011 11011
            첫구간 11011
            두번째 11011
            세번째 00000
            네번째 11011
            다섯번째 11011

        */
        return 0;
    }


    public static void Main(string[] args)
    {
        Solution solution = new Solution();
        System.Console.WriteLine();
    }

    /*0번째 유사 칸토어 비트열 = 1
    1부터 시작하는 n번째 칸토어 비트열은 n-1번째 유사 칸토어 비트열에서 1 -> 11011로 치환
    0 -> 00000으로 치환하여 만듬
    n번째 비트열에서 특정 구간 내 1의 개수?
    n과 1의 개수가 몇 개인지 알고 싶은 구간을 나타내는 l, r
    l ~ r 구간 내 1의 개수?

    제한사항 : 1 ≤ l, r ≤ 5^n

    ==> 0번째 유사 칸토어 비트열
    1

    ==> 1번째 유사 칸토어 비트열
    11011

    ==> 2번째 유사 칸토어 비트열
    11011 11011 00000 11011 11011
    ==> [l, r] = [4,17]구간에서의 1 개수
    110 /11110110000011/ 01111011 (8개)

    ==> 3번째 유사 칸토어 비트열
    1과 0마다 5비트씩 생성 * 5 => 25 25 25 25 25 => 25*5 = 125
    ==> 5의 제곱 승으로 늘어남  ( 0 번째 : 5^0 / 1번째 : 5^1 = 5 / 2번째 : 5^2 = 25 / 3번째 : 5^3 = 125)
    */
}