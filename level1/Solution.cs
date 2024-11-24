using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Solution
{
   public int solution(string[] friends, string[] gifts)
    {
        int n = friends.Length;//친구들이름배열길이
        int[,] giftArray = new int[n,n];//선물기록저장

        //선물지수계산용
        int[] givenCount = new int[n];//선물준거 수
        int [] receivedCount = new int[n];//받은선물 수

        Dictionary<string, int> friendIndex = new Dictionary<string, int>();//친구배열인덱스 딧셔너리
        
        for(int i=0; i<n; i++)
        {
            friendIndex[friends[i]] = i;//친구이름을 키로 저장
        }
        
        foreach(string gift in gifts)//선물기록처리
        {
            string[] parts = gift.Split(' ');//준사람 받은사람 이름을 공백으로 스플릿
            int givedHuman = friendIndex[parts[0]];//준사람
            int receiver = friendIndex[parts[1]];//받음사람

            giftArray[givedHuman, receiver]++;
            givenCount[givedHuman]++;//준 선물 수 ++
            receivedCount[receiver]++;//받은 수 ++
        }

        int[] nextMountGifts = new int[n];//다음달에 받을거 선물

        for(int i=0;i<n;i++)//i가 j에게 준 선물
        {
            for(int j=i+1; j<n;j++)//j가 i에게 준 선ㅁ물
            {
                int ItoJ = giftArray[i,j];
                int JtoI = giftArray[j, i];

                if(ItoJ > JtoI)
                {
                    nextMountGifts[i]++;//i가 준게 더 많으면? i++
                }
                else if(ItoJ < JtoI)
                {
                    nextMountGifts[j]++;//j가 준게 더 많으면 j++
                }
                else//주고받은 기록x, 또는 수 같을 때-->지수가 더 큰 사람이 작은사람에게 하나 받음
                {
                    //선물지수 : 내가준거 - 받은거
                    int IGift = givenCount[i] - receivedCount[i];
                    int JGift = givenCount[j] - receivedCount[j];

                    if(IGift > JGift)//선물지수 i큼
                    {
                        nextMountGifts[i]++;
                    }
                    else if(IGift < JGift)// j큼
                    {
                        nextMountGifts[j]++;
                    }
                    //지수값 동일시 선물안받음
                }
            }
        }
        return nextMountGifts.Max();//선물가장많이받는놈 선물반환

    }
    //선물기록있으면 더 많이 준애가 다음달에 하나받음
    //ㄱ록없음, 같으면 지수가 더 큰 사람이 하나 더받음
    //지수 : 준선물수 - 받은선물수
    //A : 선물준사람 B : 선물받은사람


    static void Main(string[] args)
    {
        Solution solution = new Solution();
        string[] friends = {"muzi", "ryan", "frodo", "neo"};
        string[] gifts = {"muzi frodo", "muzi frodo", "ryan muzi", "ryan muzi", "ryan muzi", "frodo muzi", "frodo ryan", "neo muzi"};
        Console.WriteLine(solution.solution(friends, gifts)); 
    }

}