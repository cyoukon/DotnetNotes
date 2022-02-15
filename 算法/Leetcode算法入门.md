# Leetcode算法入门

## 二分查找

### [704. 二分查找](https://leetcode-cn.com/problems/binary-search/)

#### 题目

给定一个 n 个元素有序的（升序）整型数组 nums 和一个目标值 target  ，写一个函数搜索 nums 中的 target，如果目标值存在返回下标，否则返回 -1。

**示例 1:**

```
输入: nums = [-1,0,3,5,9,12], target = 9
输出: 4
解释: 9 出现在 nums 中并且下标为 4
```

**示例 2:**

```
输入: nums = [-1,0,3,5,9,12], target = 2
输出: -1
解释: 2 不存在 nums 中因此返回 -1
```

**提示：**

1. 你可以假设 nums 中的所有元素是不重复的。
2. n 将在 [1, 10000]之间。
3. nums 的每个元素都将在 [-9999, 9999]之间。

#### 题解

对于有序数组，要在数组中寻找目标值，可以考虑二分法。

- 如果中间值等于目标值，则此项即为目标
- 如果中间值小于目标值，则目标值位于中间值右侧
- 如果中间值大于目标值，则目标值位于中间值左侧

每次查找都会将查找范围缩小一半，，因此二分查找的时间复杂度是*O(log n)*，其中 n*n* 是数组的长度。

二分查找的条件是查找范围不为空，即*left*≤*right*。

```C#
public class Solution
{
    public int Search(int[] nums, int target)
    {
        var low = 0;
        var high = nums.Length - 1;
        while (low <= high)
        {
            // 虽然本题不会溢出，但是为了防止溢出，不要写 var mid = (low + high) / 2
            var mid = (low - high) / 2 + low;
            if (nums[mid] == target)
            {
                return mid;
            }
            else if (nums[mid] < target)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }
        return -1;
    }
}
```

### [278. 第一个错误的版本](https://leetcode-cn.com/problems/first-bad-version/)

#### 题目

你是产品经理，目前正在带领一个团队开发新的产品。不幸的是，你的产品的最新版本没有通过质量检测。由于每个版本都是基于之前的版本开发的，所以错误的版本之后的所有版本都是错的。

假设你有 n 个版本 [1, 2, ..., n]，你想找出导致之后所有版本出错的第一个错误的版本。

你可以通过调用 `bool isBadVersion(version)` 接口来判断版本号 `version` 是否在单元测试中出错。实现一个函数来查找第一个错误的版本。你应该尽量减少对调用 API 的次数。

**示例 1：**

```
输入：n = 5, bad = 4
输出：4
解释：
调用 isBadVersion(3) -> false 
调用 isBadVersion(5) -> true 
调用 isBadVersion(4) -> true
所以，4 是第一个错误的版本。
```

**示例 2：**

```
输入：n = 1, bad = 1
输出：1
```


提示：

- 1 <= bad <= n <= 2^31^ - 1

####   题解

```C#
/* The isBadVersion API is defined in the parent class VersionControl.
      bool IsBadVersion(int version); */
public class Solution : VersionControl
{
    public int FirstBadVersion(int n)
    {
        var low = 1;
        var high = n;
        while (low < high) // 循环直至区间左右端点相同
        {
            int mid = low + (high - low) / 2; // 防止计算时溢出
            if (IsBadVersion(mid))
            {
                high = mid; // 答案在区间 [low, mid] 中
            }
            else
            {
                low = mid + 1;
            }
        }
        // 此时有 low == high，区间缩为一个点，即为答案
        return low;
    }
}
```

### [35. 搜索插入位置](https://leetcode-cn.com/problems/search-insert-position/)

#### 题目

给定一个排序数组和一个目标值，在数组中找到目标值，并返回其索引。如果目标值不存在于数组中，返回它将会被按顺序插入的位置。

请必须使用时间复杂度为 O(log n) 的算法。

**示例 1:**

```
输入: nums = [1,3,5,6], target = 5
输出: 2
```

**示例 2:**

```
输入: nums = [1,3,5,6], target = 2
输出: 1
```

**示例 3:**

```
输入: nums = [1,3,5,6], target = 7
输出: 4
```

**示例 4:**

```
输入: nums = [1,3,5,6], target = 0
输出: 0
```

**示例 5:**

```
输入: nums = [1], target = 0
输出: 0
```

**提示:**

- 1 <= nums.length <= 10^4^
- -10^4^ <= nums[i] <= 10^4^
- nums 为无重复元素的升序排列数组
- -10^4^ <= target <= 10^4^

#### 题解

```C#
public class Solution
{
    public int SearchInsert(int[] nums, int target)
    {
        var low = 0;
        var high = nums.Length - 1;
        while (low <= high)
        {
            var mid = low + (high - low) / 2;
            if (nums[mid] == target)
            {
                // 找到目标值，直接返回
                return mid;
            }
            else if (nums[mid] < target)
            {
                low = mid + 1;
            }
            else
            {
                high = mid - 1;
            }
        }
        // 没找到目标值，考虑三种情况
        // 1. 目标比数组最小值小，返回0
        // 2. 目标比数组最大值大，返回数组长度
        // 3. 目标在数组最大值和最小值之间，如【1,2,3,4,5,7】和 6，返回 5
        // 综合以上情况，返回 low 即可
        return low;
    }
}
```

