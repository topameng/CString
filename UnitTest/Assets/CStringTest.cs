using UnityEngine;
using System.Collections;
using System;

public class CStringTest : MonoBehaviour
{
    void Start()
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append('\'');
        string str = sb.ToString();
        Debug.Log(str);
        //char sep = (char)0x85;
        //str = str.Replace(',', sep);
        //string[] ss = str.Split();

        //for(int i = 0; i < ss.Length; i++)
        //{
        //    Debug.Log(ss[i]);
        //}

        TestEquals();
        TestEmptyOrNull();

        CString cs = "test/?.lua";
        cs = cs.Replace("?.lua", "TestLoader.lua");
        Debug.Log(cs.ToString());
    }

    void TestEquals()
    {
        CString cs = "hello unity";
        string str = "hello unity";
        string str1 = "hello";

        if (cs == str)
        {
            Debug.Log("operator==(CString a, string b) ok");
        }
        else
        {
            Debug.LogError("operator==(CString a, string b) failed");
        }

        if (cs != str1)
        {
            Debug.Log("operator!=(CString a, string b) ok");
        }
        else
        {
            Debug.LogError("operator!=(CString a, string b) failed");
        }

        if (str == cs)
        {
            Debug.Log("operator==(string a, CString b) ok");
        }
        else
        {
            Debug.LogError("operator==(string a, CString b) failed");
        }

        if (str1 != cs)
        {
            Debug.Log("operator!=(string a, CString b) ok");
        }
        else
        {
            Debug.LogError("operator!=(string a, CString b) failed");
        }

        if (cs.Equals(str) && !cs.Equals(str1))
        {
            Debug.Log("CString.Equals(string str) ok");
        }
        else
        {
            Debug.LogError("CString.Equals(string str) failed");
        }

        if (str.Equals(cs)) //无法扩展替换，因为被 Equals(object obj) 先匹配掉了
        {
            Debug.Log("String.Equals(CString cs) ok");
        }
        else
        {
            Debug.LogError("String.Equals(CString cs) failed: sorry can't override this operator, don't use as this");
        }
    }

    void TestEmptyOrNull()
    {
        CString str = new CString(0);

        if (CString.IsNullOrEmpty(null) && CString.IsNullOrEmpty(str))
        {
            Debug.Log("CString.IsNullOrEmpty(CString cs) ok");
        }
        else
        {
            Debug.LogError("CString.IsNullOrEmpty(CString cs) failed");
        }

        if (CString.IsNullOrWhiteSpace(null) && CString.IsNullOrWhiteSpace(" "))
        {
            Debug.Log("CString.IsNullOrWhiteSpace(CString cs) ok");
        }
        else
        {
            Debug.LogError("CString.IsNullOrWhiteSpace(CString cs) failed");
        }

        str = null;
        CString str1 = null;

        if (str == null && str == str1 && CString.Equals(str, null))
        {
            Debug.Log("CString compare to null ok");
        }
        else
        {
            Debug.LogError("CString compare to null failed");
        }
    }

    void TestToLower()
    {

    }

    void TestToUpper()
    {

    }

    void TestReplace()
    {

    }

    private void Update()
    {
        //Profiler.BeginSample("CString");
        //using (CString.Block())
        //{
        //}

        //Profiler.EndSample();
        string hello = StringPool.Alloc(5);
        TestBlock(hello);
        StringPool.Collect(hello);
    }

    string TestBlock(string str)
    {
        using (CString.Block())
        {
            CString sb = CString.Alloc(256);            
            sb.Append("hello");
            sb.CopyToString(str);
            return str;
        }
    }

}