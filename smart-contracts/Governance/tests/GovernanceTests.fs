module GovernanceTests

open System
//open System.Exception
open System.Numerics
open Xunit
open FsUnit.Xunit
open FsUnit.CustomMatchers
open Constants
open TestBase
open GovernanceTestBase
open Nethereum.Web3
open Nethereum.Hex.HexConvertors.Extensions
open Nethereum.Web3.Accounts
open Nethereum.RPC.Eth.DTOs
open Nethereum.Contracts
open SolidityTypes
open AbiTypeProvider.Common

type System.String with
   member s1.icompare(s2: string) =
     System.String.Equals(s1, s2, StringComparison.CurrentCultureIgnoreCase);

[<Specification("gFry", "constructor", 0)>]
[<Fact>]
let ``initializes with correct initial supply`` () =
    restore ()

    let gFryCon = getGFryContract()

    let zero = bigint 0;
    should equal zero (gFryCon.totalSupplyQuery())

[<Specification("gFry", "mint", 0)>]
[<Fact>]
let ``Non gFry deployer account can not mint`` () =
    restore ()

    let gFryCon = getGFryContract()
    let account = Account(hardhatPrivKey2)

    let debug = Debug(EthereumConnection(hardhatURI, account.PrivateKey))
    let data = gFryCon.mintData(account.Address, "10")

    let receipt = debug.Forward(gFryCon.Address,  data)
    let forwardEvent = debug.DecodeForwardedEvents receipt |> Seq.head
    forwardEvent |> shouldRevertWithMessage "Comp::_mint: That account cannot mint"
    let zero = bigint 0;
    should equal zero (gFryCon.totalSupplyQuery())

[<Specification("gFry", "mint", 1)>]
[<Fact>]
let ``Can't mint to the zero address`` () =
    restore ()

    let gFryCon = getGFryContract()

    try
        gFryCon.mint(zeroAddress, "A") |> ignore
        failwith "Should not be able to mint to zero address"
    with ex ->
        //printfn "%O" ex
        ex.Message.ToLowerInvariant().Contains("cannot mint to the zero address") 
        |> should equal true

// [<Specification("gFry", "mint", 1)>]
// [<Fact>]
// let ``Can't mint bigger than uint96 max`` () =
//     restore ()

//     let gFryCon = getGFryContract()
//     let smallNumber = 1000.0
//     let bignumber = (7.922816251**28.0).ToString()
//     let hexStringMax = "ffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffffff"
//     let hexStringOne = "ffffffffffffffffffffffff"
//     let hexStringTwo = "1000000000000000000000000"
//     //let bignumberStr = bignumber.ToString
//     // try
//     gFryCon.mint(hardhatAccount, hexStringTwo) |> ignore
//     // gFryCon.mint(hardhatAccount, hexStringTwo) |> ignore

//     let gFryBalance = gFryCon.balanceOfQuery(hardhatAccount)
    
//     printfn "gFry Balance %O" gFryBalance
//     //     failwith "Should not be able to mint to zero address"
//     // with ex ->
//     //     printfn "%O" ex
//     //     ex.Message.ToLowerInvariant().Contains("cannot mint to the zero address") 
//     //     |> should equal true

[<Specification("gFry", "mint", 1)>]
[<Fact>]
let ``Can mint positive amount`` () =
    restore ()

    let gFryConnection =  Contracts.gFRYContract(ethConn.GetWeb3)
    let compareBigInt = bigint 16;
    let toMint = 10.0
    let amountToMint = toMint
    let amountToMintStr = amountToMint.ToString()
    let mintTx = gFryConnection.mint(hardhatAccount,  amountToMintStr)
    mintTx |> shouldSucceed
    let gFryBalance = gFryConnection.balanceOfQuery(hardhatAccount)
    printfn "gFry Balance %O" mintTx

    let event = mintTx.DecodeAllEvents<Contracts.gFRYContract.TransferEventDTO>() |> Seq.map (fun i -> i.Event) |> Seq.head
    event.from |> should equal zeroAddress
    event.amount |> should equal compareBigInt
    gFryBalance |> should equal compareBigInt

[<Specification("gFry", "burn", 1)>]
[<Fact>]
let ``Can't burn of not deployer`` () =
    restore ()

    let gFryConnection =  Contracts.gFRYContract(ethConn.GetWeb3)
    let compareBigInt = bigint 16;
    let toMint = 100.0
    let amountToMint = toMint
    let amountToMintStr = amountToMint.ToString()
    let mintTx = gFryConnection.mint(hardhatAccount,  amountToMintStr)

    gFryConnection.balanceOfQuery(hardhatAccount)
    |> printfn "Post Mint Balance: %O" 
    gFryConnection.totalSupplyQuery()
    |> printfn "Post total Supply: %O" 

    let burnTx = gFryConnection.burn("10")
    gFryConnection.balanceOfQuery(hardhatAccount)
    |> printfn "Post Burn Balance: %O" 
    gFryConnection.totalSupplyQuery()
    |> printfn "Post total Supply: %O" 

    // let gFryCon = getGFryContract()
    // let account = Account(hardhatPrivKey2)

    // let debug = Debug(EthereumConnection(hardhatURI, account.PrivateKey))
    // let data = gFryConnection.burnData("1")

    // let receipt = debug.Forward(gFryConnection.Address,  data)
    // let forwardEvent = debug.DecodeForwardedEvents receipt |> Seq.head
    // forwardEvent |> shouldRevertWithMessage "Comp::_mint: That account cannot mint"
    // should equal compareBigInt (gFryConnection.totalSupplyQuery())


    // let redeemerConnection = EthereumConnection(hardhatURI, hardhatPrivKey2)

    // let tokensToTransferBigInt = tokensToMint |> toE18
    // let tokensToRedeemBigInt = tokensToRedeem |> toE18

    // tokensToRedeemBigInt |> should lessThan tokensToTransferBigInt

    // dEthContract.transfer(redeemerConnection.Account.Address,tokensToTransferBigInt) |> shouldSucceed

    // let tokenBalanceBefore = balanceOf dEthContract redeemerConnection.Account.Address

    // let gulperBalanceBefore = getGulperEthBalance ()

    // if riskLevelShouldBeExceeded then
    //     makeRiskLimitLessThanExcessCollateral dEthContract |> shouldSucceed

    // let receiverAddress = makeAccount().Address
    
    // let (protocolFeeExpected, automationFeeExpected, collateralRedeemedExpected, collateralReturnedExpected) = 
    //     queryStateAndCalculateRedemptionValue dEthContract tokensToRedeemBigInt

    // let redeemerContract = Contracts.dEthContract(dEthContract.Address, redeemerConnection.GetWeb3)
    // let redeemTx = redeemerContract.redeem(receiverAddress,tokensToRedeemBigInt)
    // redeemTx |> shouldSucceed

    // receiverAddress |> ethConn.GetEtherBalance |> should equal collateralReturnedExpected
    // getGulperEthBalance () |> should equal (protocolFeeExpected + gulperBalanceBefore)

    // balanceOf dEthContract redeemerConnection.Account.Address |> should equal (tokenBalanceBefore - tokensToRedeemBigInt)

    // let event = redeemTx.DecodeAllEvents<Contracts.dEthContract.RedeemedEventDTO>() |> Seq.map (fun i -> i.Event) |> Seq.head
    // event._redeemer |> shouldEqualIgnoringCase redeemerConnection.Account.Address
//     event._receiver |> shouldEqualIgnoringCase receiverAddress
//     event._tokensRedeemed |> should equal tokensToRedeemBigInt
//     event._protocolFee |> should equal protocolFeeExpected
//     event._automationFee |> should equal automationFeeExpected
//     event._collateralRedeemed |> should equal collateralRedeemedExpected
//     event._collateralReturned |> should equal collateralReturnedExpected








    // let data = gFryCon.mint(zeroAddress, "A") |> shouldFail
    // //printfn "TX: %O" data.TransactionHash

    // let zero = bigint 0;
    // should equal zero (gFryCon.totalSupplyQuery())


    //printfn "TX: %O" data.TransactionHash
    // let trydivisionFunc x y =
    //     try
    //         gFryCon.mint("0x0000000000000000000000000000000000000000", "10")
    //     with
    //         | Failure()
    //1
    //( gFryCon.mint("0x0000000000000000000000000000000000000000", "10") -> This.Throws("a", 10) |> ignore)
    //    |> should (throwWithMessage "Some message") typeof<ArgumentException> 

    // try
    //     gFryCon.mint("0x0000000000000000000000000000000000000000", "10")
    // finally
    //     1

    // let receipt = debug.Forward(gFryCon.Address, data)
    // let forwardEvent = debug.DecodeForwardedEvents receipt |> Seq.head
    // let result = forwardEvent |> shouldSucceed

    
    // let debug = Debug(EthereumConnection(hardhatURI, account.PrivateKey))
    // let data = gFryCon.mintData(account2.Address, "A")
    // debug.Forward(gFryCon.Address, data)
    //     |> debug.DecodeForwardedEvents
    //     |> Seq.head
    //     |> printfn "%O"

    
    //let receipt = debug.Forward(gFryCon.Address,  data)
    //let forwardEvent = debug.DecodeForwardedEvents receipt |> Seq.head
    //forwardEvent |> shouldRevertWithMessage "Comp::_mint: That account cannot mint"
    // printf "RETURN DATA:"
    // printfn "%O" data
    // debug.Forward(dEthContract.Address, data)
    //     |> debug.DecodeForwardedEvents
    //     |> Seq.head
    //     |> shouldRevertWithUnknownMessage
    
    // let zero = bigint 0;
    // should equal zero (gFryCon.totalSupplyQuery())
   

// [<Specification("dEth", "changeGulper", 1)>]
// [<Fact>]
// let ``cannot be changed by non-owner`` () = 
//     restore ()
//     let contract = getDEthContract ()
//     let account = Account(hardhatPrivKey2)
//     let oldGulper = contract.gulperQuery()

//     let debug = Debug(EthereumConnection(hardhatURI, account.PrivateKey))
//     let data = contract.changeGulperData(account.Address)
//     let receipt = debug.Forward(contract.Address,  data)
//     let forwardEvent = debug.DecodeForwardedEvents receipt |> Seq.head
//     forwardEvent |> shouldRevertWithUnknownMessage
//     shouldEqualIgnoringCase oldGulper <| contract.gulperQuery()

// [<Specification("dEth", "constructor", 0)>]
// [<Fact>]
// let ``initializes with correct values and rights assigned`` () =
//     restore ()

//     let authority, contract = getDEthContractAndAuthority()

//     // check the rights
//     let functionName = Web3.Sha3("changeSettings(uint256,uint256,uint256)").Substring(0, 8).HexToByteArray()
//     let canCall = authority.canCallQuery (foundryTreasury, contract.Address, functionName)

//     // check the balance of initialRecipient
//     let balanceOfInitialRecipient = contract.balanceOfQuery(initialRecipient)

//     shouldEqualIgnoringCase gulper <| contract.gulperQuery()
//     shouldEqualIgnoringCase proxyCache <| contract.cacheQuery()
//     should equal cdpId <| contract.cdpIdQuery()
//     shouldEqualIgnoringCase makerManager <| contract.makerManagerQuery()
//     shouldEqualIgnoringCase ethGemJoin <| contract.ethGemJoinQuery()
//     shouldEqualIgnoringCase saverProxy <| contract.saverProxyQuery()
//     shouldEqualIgnoringCase saverProxyActions <| contract.saverProxyActionsQuery()
//     shouldEqualIgnoringCase oracleContractMainnet.Address <| contract.oracleQuery()
//     should be True canCall
//     should greaterThan BigInteger.Zero balanceOfInitialRecipient
//     dEthContract.minRedemptionRatioQuery() |> should equal <| (bigint 160) * ratio

// [<Specification("Oracle", "constructor", 0)>]
// [<Fact>]
// let ``inits to provided parameters`` () =
//     restore ()

//     let (makerOracle, daiUsdOracle, ethUsdOracle) = (makeAccount().Address, makeAccount().Address, makeAccount().Address)
//     let contract = Contracts.OracleContract(ethConn.GetWeb3, makerOracle, daiUsdOracle, ethUsdOracle)

//     shouldEqualIgnoringCase makerOracle (contract.makerOracleQuery ())
//     shouldEqualIgnoringCase daiUsdOracle (contract.daiUsdOracleQuery ())
//     shouldEqualIgnoringCase ethUsdOracle (contract.ethUsdOracleQuery ())

// [<Specification("Oracle", "getEthDaiPrice", 0)>]
// [<Theory>]
// [<InlineData(0.08)>]
// [<InlineData(0.1)>]
// [<InlineData(0.12)>]
// let ``price is correct given source prices within ten percents of one another`` differencePercent =
//     restore ()

//     let (priceMaker, _, priceNonMakerDaiEth, _) = initOraclesDefault differencePercent

//     let price = oracleContract.getEthDaiPriceQueryAsync() |> runNow

//     let expected =
//         if differencePercent <= 0.1M
//         then 
//             toMakerPriceFormatDecimal priceNonMakerDaiEth
//         else 
//             toMakerPriceFormatDecimal priceMaker

//     should equal expected price

// [<Specification("dEth", "constructor", 0)>]
// [<Fact>]
// let ``initializes with correct values and rights assigned`` () =
//     restore ()

//     let authority, contract = getDEthContractAndAuthority()

//     // check the rights
//     let functionName = Web3.Sha3("changeSettings(uint256,uint256,uint256)").Substring(0, 8).HexToByteArray()
//     let canCall = authority.canCallQuery (foundryTreasury, contract.Address, functionName)

//     // check the balance of initialRecipient
//     let balanceOfInitialRecipient = contract.balanceOfQuery(initialRecipient)

//     shouldEqualIgnoringCase gulper <| contract.gulperQuery()
//     shouldEqualIgnoringCase proxyCache <| contract.cacheQuery()
//     should equal cdpId <| contract.cdpIdQuery()
//     shouldEqualIgnoringCase makerManager <| contract.makerManagerQuery()
//     shouldEqualIgnoringCase ethGemJoin <| contract.ethGemJoinQuery()
//     shouldEqualIgnoringCase saverProxy <| contract.saverProxyQuery()
//     shouldEqualIgnoringCase saverProxyActions <| contract.saverProxyActionsQuery()
//     shouldEqualIgnoringCase oracleContractMainnet.Address <| contract.oracleQuery()
//     should be True canCall
//     should greaterThan BigInteger.Zero balanceOfInitialRecipient
//     dEthContract.minRedemptionRatioQuery() |> should equal <| (bigint 160) * ratio

// [<Specification("dEth", "changeGulper", 0)>]
// [<Fact>]
// let ``can be changed by owner`` () =
//     restore ()
//     let contract = getDEthContract ()
//     let randomAddress = makeAccount().Address
//     contract.changeGulper(randomAddress) |> ignore
//     shouldEqualIgnoringCase randomAddress <| contract.gulperQuery()

// let giveCDPToDSProxyTestBase shouldThrow = 
//     restore ()
//     let newContract = getDEthContract ()

//     let executeGiveCDPFromPrivateKey shouldThrow =
//         if shouldThrow then 
//             let debug = Debug(EthereumConnection(hardhatURI, hardhatPrivKey2))
//             let data = dEthContract.giveCDPToDSProxyData(newContract.Address)
//             debug.Forward(newContract.Address, data)
//         else 
//             dEthContract.giveCDPToDSProxy(newContract.Address)
    
//     let giveCDPToDSProxyReceipt = executeGiveCDPFromPrivateKey shouldThrow

//     if shouldThrow then
//         let forwardEvent = debug.DecodeForwardedEvents giveCDPToDSProxyReceipt |> Seq.head
//         forwardEvent |> shouldRevertWithUnknownMessage
//     else
//         giveCDPToDSProxyReceipt.Succeeded () |> should equal true
//         dEthContract.riskLimitQuery() |> should equal (BigInteger 0)


// [<Specification("dEth", "giveCDPToDSProxy", 0)>]
// [<Fact>]
// let ``dEth - giveCDPToDSProxy - can be called by owner`` () = giveCDPToDSProxyTestBase false

// [<Specification("dEth", "giveCDPToDSProxy", 1)>]
// [<Fact>]
// let ``dEth - giveCDPToDSProxy - cannot be called by non-owner`` () = giveCDPToDSProxyTestBase true

// [<Specification("dEth", "getCollateral", 0)>]
// [<Fact>]
// let ``dEth - getCollateral - returns similar values as those directly retrieved from the underlying contracts and calculated in F#`` () = 
//     restore ()
//     let contract = getDEthContract ()

//     let getCollateralOutput = contract.getCollateralQuery()
//     let (_, priceRay, _, cdpDetailedInfoOutput, collateralDenominatedDebt, excessCollateral) = 
//         getManuallyComputedCollateralValues oracleContractMainnet saverProxy cdpId
    
//     should equal priceRay getCollateralOutput._priceRAY
//     should equal cdpDetailedInfoOutput.collateral getCollateralOutput._totalCollateral
//     should equal cdpDetailedInfoOutput.debt getCollateralOutput._debt
//     should equal collateralDenominatedDebt getCollateralOutput._collateralDenominatedDebt
//     should equal excessCollateral getCollateralOutput._excessCollateral

// [<Specification("dEth", "getCollateralPriceRAY", 0)>]
// [<Fact>]
// let ``dEth - getCollateralPriceRAY - returns similar values as those directly retrieved from the underlying contracts and calculated in F#`` () = 
//     restore ()
//     let contract = getDEthContract ()

//     let ethDaiPrice = oracleContractMainnet.getEthDaiPriceQuery()
//     let expectedRay = BigInteger.Pow(bigint 10, 9) * ethDaiPrice

//     let actualRay = contract.getCollateralPriceRAYQuery()
//     should equal expectedRay actualRay

// [<Specification("dEth", "getExcessCollateral", 0)>]
// [<Fact>]
// let ``dEth - getExcessCollateral - returns similar values as those directly retrieved from the underlying contracts and calculated in F#`` () =
//     restore ()
//     let contract = getDEthContract ()

//     let (_, _, _, _, _, excessCollateral) = getManuallyComputedCollateralValues oracleContractMainnet saverProxy cdpId

//     let actual = contract.getExcessCollateralQuery()
//     should equal excessCollateral actual

// [<Specification("dEth", "getRatio", 0)>]
// [<Fact>]
// let ``dEth - getRatio - returns similar values as those directly retrieved from the underlying contracts and calculated in F#`` () =
//     restore ()
//     let contract = getDEthContract ()
//     let saverProxyContract = Contracts.MCDSaverProxyContract(saverProxy, ethConn.GetWeb3)
//     let manager = Contracts.ManagerLikeContract(makerManager, ethConn.GetWeb3)

//     let ilk = manager.ilksQuery(cdpId)
//     let price = saverProxyContract.getPriceQuery (ilk)
//     let getCdpInfoOutputDTO = saverProxyContract.getCdpInfoQuery(manager.Address,cdpId,ilk)

//     let expected = 
//         if getCdpInfoOutputDTO.Prop1 = BigInteger.Zero 
//         then 
//             BigInteger.Zero 
//         else 
//             rdiv (wmul getCdpInfoOutputDTO.Prop0 price) getCdpInfoOutputDTO.Prop1

//     let actual = contract.getRatioQuery()

//     should equal expected actual

// [<Specification("dEth", "changeSettings", 0)>]
// [<Theory>]
// [<InlineData(foundryTreasury, 180, 220, 220, 1, 1, 1)>]
// [<InlineData(ownerArg, 180, 220, 220, 1, 1, 1)>]
// [<InlineData(contractArg, 180, 220, 220, 1, 1, 1)>]
// let ``dEth - changeSettings - an authorised address can change the settings`` (addressArgument:string) (repaymentRatioExpected:int) (targetRatioExpected:int) (boostRatioExpected:int) (minRedemptionRatioExpected:int) (automationFeePercExpected:int) (riskLimitExpected:int) =
//     restore ()

//     let changeSettingsTxr = 
//         Contracts.dEthContract.changeSettingsFunction(
//             _minRedemptionRatio = bigint minRedemptionRatioExpected,
//             _automationFeePerc = bigint automationFeePercExpected, 
//             _riskLimit = bigint riskLimitExpected)
//         |> ethConn.MakeImpersonatedCallWithNoEther (mapInlineDataArgumentToAddress addressArgument dEthContract.Address) dEthContract.Address

//     changeSettingsTxr |> shouldSucceed

//     dEthContract.minRedemptionRatioQuery() |> should equal <| (bigint minRedemptionRatioExpected) * ratio
//     dEthContract.automationFeePercQuery() |> should equal (bigint automationFeePercExpected)
//     dEthContract.riskLimitQuery() |> should equal (bigint riskLimitExpected)

//     let event = changeSettingsTxr.DecodeAllEvents<Contracts.dEthContract.SettingsChangedEventDTO>() |> Seq.map (fun i -> i.Event) |> Seq.head
//     event._minRedemptionRatio |> should equal <| (bigint minRedemptionRatioExpected) * ratio
//     event._automationFeePerc |> should equal <| bigint automationFeePercExpected
//     event._riskLimit |> should equal <| bigint riskLimitExpected

// [<Specification("dEth", "changeSettings", 1)>]
// [<Theory>]
// [<InlineData(repaymentRatio, targetRatio, boostRatio, 1, 1, 1)>]
// let ``dEth - changeSettings - an unauthorised address cannot change the automation settings`` (repaymentRatioExpected:int) (targetRatioExpected:int) (boostRatioExpected:int) (minRedemptionRatioExpected:int) (automationFeePercExpected:int) (riskLimitExpected:int) = 
//     restore ()

//     let debug = Debug(EthereumConnection(hardhatURI, makeAccountWithBalance().PrivateKey))
//     let data = dEthContract.changeSettingsData(bigint minRedemptionRatioExpected, bigint automationFeePercExpected, bigint riskLimitExpected)

//     debug.Forward(dEthContract.Address, data)
//     |> debug.DecodeForwardedEvents
//     |> Seq.head
//     |> shouldRevertWithUnknownMessage // To clarify : We get no message because the auth code reverts without providing one

// [<Specification("dEth", "redeem", 0)>]
// [<Theory>]
// [<InlineData(10.0, 7.0, false)>]
// [<InlineData(1.0, 0.7, false)>]
// [<InlineData(0.01, 0.005, false)>]
// [<InlineData(0.001, 0.0005, false)>]
// [<InlineData(10.0, 7.0, true)>]
// [<InlineData(1.0, 0.05, true)>]
// let ``blah dEth - redeem - someone with a positive balance of dEth can redeem the expected amount of Ether`` (tokensToMint:float) (tokensToRedeem:float) (riskLevelShouldBeExceeded:bool) =
//     restore ()

//     let redeemerConnection = EthereumConnection(hardhatURI, hardhatPrivKey2)

//     let tokensToTransferBigInt = tokensToMint |> toE18
//     let tokensToRedeemBigInt = tokensToRedeem |> toE18

//     tokensToRedeemBigInt |> should lessThan tokensToTransferBigInt

//     dEthContract.transfer(redeemerConnection.Account.Address,tokensToTransferBigInt) |> shouldSucceed

//     let tokenBalanceBefore = balanceOf dEthContract redeemerConnection.Account.Address

//     let gulperBalanceBefore = getGulperEthBalance ()

//     if riskLevelShouldBeExceeded then
//         makeRiskLimitLessThanExcessCollateral dEthContract |> shouldSucceed

//     let receiverAddress = makeAccount().Address
    
//     let (protocolFeeExpected, automationFeeExpected, collateralRedeemedExpected, collateralReturnedExpected) = 
//         queryStateAndCalculateRedemptionValue dEthContract tokensToRedeemBigInt

//     let redeemerContract = Contracts.dEthContract(dEthContract.Address, redeemerConnection.GetWeb3)
//     let redeemTx = redeemerContract.redeem(receiverAddress,tokensToRedeemBigInt)
//     redeemTx |> shouldSucceed

//     receiverAddress |> ethConn.GetEtherBalance |> should equal collateralReturnedExpected
//     getGulperEthBalance () |> should equal (protocolFeeExpected + gulperBalanceBefore)

//     balanceOf dEthContract redeemerConnection.Account.Address |> should equal (tokenBalanceBefore - tokensToRedeemBigInt)

//     let event = redeemTx.DecodeAllEvents<Contracts.dEthContract.RedeemedEventDTO>() |> Seq.map (fun i -> i.Event) |> Seq.head
//     event._redeemer |> shouldEqualIgnoringCase redeemerConnection.Account.Address
//     event._receiver |> shouldEqualIgnoringCase receiverAddress
//     event._tokensRedeemed |> should equal tokensToRedeemBigInt
//     event._protocolFee |> should equal protocolFeeExpected
//     event._automationFee |> should equal automationFeeExpected
//     event._collateralRedeemed |> should equal collateralRedeemedExpected
//     event._collateralReturned |> should equal collateralReturnedExpected

// [<Specification("dEth", "redeem", 1)>]
// [<Theory>]
// [<InlineData(10000)>]
// let ``dEth - redeem - someone without a balance can never redeem Ether`` (tokensAmount: int) =
//     restore ()

//     let debug = Debug(EthereumConnection(hardhatURI, makeAccountWithBalance().PrivateKey)) // the balance is needed for gas vs for sending ether value.
//     let data = dEthContract.redeemData(makeAccount().Address, bigint tokensAmount)
    
//     debug.Forward(dEthContract.Address, data)
//     |> debug.DecodeForwardedEvents
//     |> Seq.head
//     |> shouldRevertWithMessage "ERC20: burn amount exceeds balance"

// [<Specification("dEth", "squanderMyEthForWorthlessBeansAndAgreeToTerms", 1)>]
// [<Theory>]
// [<InlineData(100.0)>]
// [<InlineData(10.0)>]
// [<InlineData(1.0)>]
// [<InlineData(0.01)>]
// [<InlineData(0.001)>]
// [<InlineData(0.0001)>]
// [<InlineData(0.0)>] // a test case checking that no-one providing no ether can issue themselves any dEth
// let ``dEth - squanderMyEthForWorthlessBeansAndAgreeToTerms - anyone providing a positive balance of Ether can issue themselves the expected amount of dEth`` (providedCollateral:float) =
//     restore ()

//     let providedCollateralBigInt = bigint providedCollateral

//     let inkBefore = getInk ()
//     let gulperBalanceBefore = getGulperEthBalance ()
    
//     dEthContract.getExcessCollateralQuery()
//     |> should lessThan (dEthContract.riskLimitQuery() + providedCollateralBigInt)
    
//     let (protocolFeeExpected, automationFeeExpected, actualCollateralAddedExpected, accreditedCollateralExpected, tokensIssuedExpected) = 
//         queryStateAndCalculateIssuanceAmount dEthContract providedCollateralBigInt

//     let dEthRecipientAddress = ethConn.Account.Address
//     let balanceBefore = balanceOf dEthContract dEthRecipientAddress

//     let squanderTxr = dEthContract.squanderMyEthForWorthlessBeansAndAgreeToTerms(dEthRecipientAddress, weiValue providedCollateralBigInt)
//     squanderTxr |> shouldSucceed

//     balanceOf dEthContract dEthRecipientAddress |> should equal (balanceBefore + tokensIssuedExpected)
//     getInk () |> should equal (inkBefore + actualCollateralAddedExpected)
//     getGulperEthBalance () |> should equal (gulperBalanceBefore + protocolFeeExpected)

//     let issuedEvent = squanderTxr.DecodeAllEvents<Contracts.dEthContract.IssuedEventDTO>() |> Seq.map (fun i -> i.Event) |> Seq.head

//     issuedEvent._receiver |> shouldEqualIgnoringCase dEthRecipientAddress
//     issuedEvent._suppliedCollateral |> should equal providedCollateralBigInt
//     issuedEvent._protocolFee |> should equal protocolFeeExpected
//     issuedEvent._automationFee |> should equal automationFeeExpected
//     issuedEvent._actualCollateralAdded |> should equal actualCollateralAddedExpected
//     issuedEvent._accreditedCollateral |> should equal accreditedCollateralExpected
//     issuedEvent._tokensIssued |> should equal tokensIssuedExpected

// [<Specification("dEth", "squanderMyEthForWorthlessBeansAndAgreeToTerms", 2)>]
// [<Fact>]
// let ``dEth - squanderMyEthForWorthlessBeansAndAgreeToTerms - the riskLevel cannot be exceeded`` () =
//     restore ()

//     makeRiskLimitLessThanExcessCollateral dEthContract |> shouldSucceed

//     let data = dEthContract.squanderMyEthForWorthlessBeansAndAgreeToTermsData(makeAccount().Address)
//     debug.Forward(dEthContract.Address, data)
//     |> debug.DecodeForwardedEvents
//     |> Seq.head
//     |> shouldRevertWithMessage "risk limit exceeded"