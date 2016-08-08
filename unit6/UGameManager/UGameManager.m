//
//  UGameManager.m
//  UGameManager
//
//  Created by Li Zhengyan on 7/13/16.
//  Copyright © 2016 YXW. All rights reserved.
//

#import "UGameManager.h"
#import <UIKit/UIKit.h>


@implementation UGameManager
static UGameManager* gameMgr=nil;
@synthesize isProcessPayments;




- (id)init
{
    self = [super init];
    if (self) {
        // Initialization code here.
    }
    
    return self;
}

- (void) dealloc
{
    [super dealloc];
}

+ (UGameManager*) instance
{
    if (gameMgr==nil)
    {
        gameMgr=[[UGameManager alloc]init];
    }
    
    return gameMgr;
}

-(void) ShowWaringBox:(NSString *)strTitle :(NSString *)strText
{
    UIAlertView * alertview = [[UIAlertView alloc]initWithTitle:strTitle message:strText delegate:self cancelButtonTitle:@"取消" otherButtonTitles:@"确定", nil];
    [alertview show];
    [alertview release];
}


- (void) iniStoreKit
{
    [[SKPaymentQueue defaultQueue] addTransactionObserver:self];
}

- (BOOL) canProcessPayments
{
    if([SKPaymentQueue canMakePayments])
    {
        return YES;
    }else{
        return NO;
    }
}

- (void) purchaseItem:(NSString *)identifier
{
    isProcessPayments=YES;
    SKPayment *payment = [SKPayment paymentWithProductIdentifier:identifier];
    [[SKPaymentQueue defaultQueue] addPayment:payment];
}

- (void)paymentQueue:(SKPaymentQueue *)queue updatedTransactions:(NSArray*) transactions
{
    for (SKPaymentTransaction *transaction  in transactions)
    {
        switch (transaction.transactionState) {
            case SKPaymentTransactionStatePurchased:
                [self completeTransaction:transaction];
                break;
            case SKPaymentTransactionStateFailed:
                [self failedTransaction:transaction];
                break;
            case SKPaymentTransactionStateRestored:
                [self restoreTransaction:transaction];
                break;
                
            default:
                break;
        }
    }
}

- (void) completeTransaction:(SKPaymentTransaction *)transaction
{
    isProcessPayments=NO;
    [self recordTransaction:transaction];
    [self provideContent:transaction.payment.productIdentifier];
    [[SKPaymentQueue defaultQueue]finishTransaction:transaction];
    
}

- (void) restoreTransaction:(SKPaymentTransaction *)transaction
{
    isProcessPayments=NO;
    [self recordTransaction:transaction];
    [self provideContent:transaction.originalTransaction.payment.productIdentifier];
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

- (void) failedTransaction:(SKPaymentTransaction *)transaction
{
    isProcessPayments=NO;
    if(transaction.error.code!=SKErrorPaymentCancelled)
    {
        UnitySendMessage("UGameObject","iOSIAPCallback",[transaction.error.localizedDescription UTF8String]);
        
    }else{
        
        UnitySendMessage("UGameObject","iOSIAPCallback","取消购买");
    }
    [[SKPaymentQueue defaultQueue] finishTransaction:transaction];
}

- (void)recordTransaction:(SKPaymentTransaction *)transaction
{
    
}

- (void) provideContent:(NSString *)identifier
{
    UnitySendMessage("UGameObject","IOSIAPCallback",[identifier UTF8String]);
}

NSString* CreateNSString (const char* string)
{
    if (string)
        return [NSString stringWithUTF8String: string];
    else
        return [NSString stringWithUTF8String: ""];
}


void _startup()
{
    [UGameManager instance];
}

void _showWarningBox(char* strTitle ,char* strText)
{
    [gameMgr ShowWaringBox:[NSString stringWithUTF8String:strTitle] :[NSString stringWithUTF8String:strText]];
}

// iap
void _iniStore()
{
    [gameMgr iniStoreKit];
}

BOOL _canProcessPayments()
{
    return [gameMgr canProcessPayments];
}

BOOL _isProcessPayments()
{
    return [gameMgr isProcessPayments];
}

void _purchaseItem( char* identifier)
{
    [gameMgr purchaseItem:CreateNSString(identifier)];
}

@end
