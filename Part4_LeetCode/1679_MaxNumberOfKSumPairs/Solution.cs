public class Solution {
    public int MaxOperations(int[] nums, int k) {
        int l = 0, r = nums.Length - 1 , ans = 0;
        nums.Sort();
        while (l < r)
        {
            int sum = nums[r] + nums[l];
            if (sum == k)
            {
                ans++;
                l++;
                r--;
            }
            else if (sum > k)
            {
                r--;
            }
            else
            {
                l++;
            }
        }
        return ans;
    }
}
