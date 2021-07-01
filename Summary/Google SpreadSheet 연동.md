# Google Spread Sheet ����

����Ƽ�� Google Spread Sheet�� �����Ͽ� ����ϴ� ����� �˾ƺ��Ҵ�.

## URL ��ũ �̿�

### ������ ���е� �� �ҷ�����

����Ƽ���� ���� ��Ʈ�� �̿��ϱ� ���ؼ� ������ ���е� ���� �ٿ�ε� �Ͽ� ����Ѵ�.  
<br>  
���������Ʈ�� `��ũ�� �ִ� ��� ����ڿ��� ����` �� ��ȯ�� �� URL�� �����´�.  
������ URL�� �� �κ��� `/edit#gid=0` ��� `/export?format=tsv`�� �ٿ��ش�.  
<br>
������,  
�ۼ����� ���������Ʈ�� URL�� `https://docs.google.com/spreadsheets/d/19fzcMGoB2JWbW75trJCH1G10k4Ffxgl6rW3LXio22X4/edit#gid=0` �� ���  
`https://docs.google.com/spreadsheets/d/19fzcMGoB2JWbW75trJCH1G10k4Ffxgl6rW3LXio22X4/export?format=tsv` �� �����´�.  
<br>
�̷��� �ش� URL�� ������ ���е� ���� �ٿ�ε� �ϰ� �Ǵ� URL�� �ȴ�.

```C#
    const string URL = "https://docs.google.com/spreadsheets/d/19fzcMGoB2JWbW75trJCH1G10k4Ffxgl6rW3LXio22X4/export?format=tsv";

    IEnumerator Start()
    {
        UnityWebRequest www = UnityWebRequest.Get(URL);
        yield return www.SendWebRequest();

        string data = www.downloadHandler.text;
        print(data);
    }
```
![Image](../SampleApp/App_SpreadSheet/Image/Sheet_1.png)
![Image2](../SampleApp/App_SpreadSheet/Image/Sheet_2.png)
* SpreadSheet ���� �ҷ����� ����!

### ���ϴ� ������ �� �ҷ�����

`������ ���е� �� �ҷ�����` ���� ����� URL�� ���κп� ������ �����Ѵ�.  
������, A2 ������ B4 �� ������ �ҷ����� ���� ���  
URL�� ���κп� `&range=A2:B4`�� �Է��Ѵ�.  
`https://docs.google.com/spreadsheets/d/19fzcMGoB2JWbW75trJCH1G10k4Ffxgl6rW3LXio22X4/export?format=tsv&range=A2:B4`

## SpreadSheet�� ��ũ��Ʈ ������ �̿�

### Script �̿�

��ũ��Ʈ�� ���� sheet�� �����Ϸ��� �ϴ� ��� �۾����� ��Ʈ�� ID�� �����´�.
�۾����� ��Ʈ�� URL�� `https://docs.google.com/spreadsheets/d/19fzcMGoB2JWbW75trJCH1G10k4Ffxgl6rW3LXio22X4/edit#gid=0` �� ���
ID�� `19fzcMGoB2JWbW75trJCH1G10k4Ffxgl6rW3LXio22X4` �� �ش��Ѵ�.

```gs
var sheetId = SpreadsheetApp.openById("19fzcMGoB2JWbW75trJCH1G10k4Ffxgl6rW3LXio22X4");
var sheet = sheetId.getSheets()[0];     //0��° ��Ʈ�� ������ ��´�.

function myFunction()
{
    sheet.getRange(2, 2).setValue("test");  //2�� 3���� �� ����
}
```


### [���� ��ũ]

* https://www.youtube.com/watch?v=3LxaTtLsC-w
