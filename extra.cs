//if (targetPos != null)
//{
//    Vector x0 = new Vector(0, 1, 0);
//    Vector y0 = new Vector(1, 0, 0);
//    Vector z0 = new Vector(1, 0, 0);

//    double xAngle = new Vector(0, direction.y, direction.z).GetAngle(x0) * (direction.z / Math.Abs(direction.z));
//    double yAngle = new Vector(direction.x, 0, direction.z).GetAngle(y0) * (direction.z / Math.Abs(direction.z));
//    double zAngle = new Vector(direction.x, direction.y, 0).GetAngle(z0) * (direction.y / Math.Abs(direction.y)); 


//    Vector targetDirection = (targetPos - position).Normalize();
//    double xAngleToMatch = new Vector(0, targetDirection.y, targetDirection.z).GetAngle(x0) * (targetDirection.z / Math.Abs(targetDirection.z));
//    double yAngleToMatch = new Vector(targetDirection.x, 0, targetDirection.z).GetAngle(y0) * (targetDirection.z / Math.Abs(targetDirection.z));
//    double zAngleToMatch = new Vector(targetDirection.x, targetDirection.y, 0).GetAngle(z0) * (targetDirection.y / Math.Abs(targetDirection.y));

//    double xRotation = rotationSpeed;
//    double yRotation = rotationSpeed;
//    double zRotation = rotationSpeed;

//    double xdiff = xAngleToMatch - xAngle;
//    if (Math.Abs(xdiff) <= rotationSpeed)
//    {
//        xRotation = xdiff;
//    }
//    else if (xdiff < 0)
//    {
//        xRotation = -rotationSpeed;
//    }


//    double ydiff = yAngleToMatch - yAngle;
//    if (Math.Abs(ydiff) <= rotationSpeed)
//    {
//        yRotation = ydiff;
//    }
//    else if (ydiff < 0)
//    {
//        yRotation = -rotationSpeed;
//    }



//    double zdiff = zAngleToMatch - zAngle;
//    if (Math.Abs(zdiff) <= rotationSpeed)
//    {
//        zRotation = zdiff;
//    }
//    else if (zdiff < 0)
//    {
//        zRotation = -rotationSpeed;
//    }



//    if (xRotation != 0)
//    {
//        Console.WriteLine("x");
//        direction.RotateX(xRotation);
//        direction = direction.Normalize();
//    }

//    if (yRotation != 0)
//    {
//        Console.WriteLine("y");
//        direction.RotateY(yRotation);
//        direction = direction.Normalize();
//    }
//    if (zRotation != 0)
//    {
//        Console.WriteLine("z");
//        direction.RotateZ(zRotation);
//        direction = direction.Normalize();
//    }
//}