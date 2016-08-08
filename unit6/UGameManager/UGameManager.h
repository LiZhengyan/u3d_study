//
//  UGameManager.h
//  UGameManager
//
//  Created by Li Zhengyan on 7/13/16.
//  Copyright © 2016 YXW. All rights reserved.
//

#import <Foundation/Foundation.h>
#import <StoreKit/StoreKit.h>

@interface UGameManager : NSObject<SKPaymentTransactionObserver>
{
    BOOL isProcessPayments; //判断交易是否进行
}

@property (nonatomic) BOOL isProcessPayments; //多线程可以访问
+ (UGameManager*) instance;
-(void) ShowWaringBox:(NSString*) strTitle:(NSString*) strText;

-(void) iniStoreKit; //初始化消费

-(BOOL) canProcessPayments;  //是否可以内消费 ？

-(void) purchaseItem:(NSString*) identifier; //购买商品

-(void) completeTransaction:(SKPaymentTransaction*) transaction; //结束交易

-(void) restoreTransaction: (SKPaymentTransaction*) transaction; //重置交易

-(void) failedTransaction:(SKPaymentTransaction*) transaction; //交易失败

-(void) recordTransaction:(SKPaymentTransaction*) transaction; //交易记录

-(void) provideContent: (NSString*) identifier; //提供商品

extern void UnitySendMessage(const char *, const char *, const char *);


void _statup(void);
void _showWarningBox(char* strTitle,char* strText);
//对unity的4个C函数

void _iniStore(void);
BOOL _canProcessPayments(void);
BOOL _isProcessPayments(void);
void _purchaseItem(char* identifier);






@end
